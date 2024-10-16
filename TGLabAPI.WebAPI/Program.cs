using Microsoft.EntityFrameworkCore;
using TGLabAPI.Infrastructure;
using TGLabAPI.Infrastructure.Repositories.Player;
using TGLabAPI.Application.Interfaces.Repositories.Player;
using TGLabAPI.Application.Interfaces.Services.Player;
using TGLabAPI.Application.Services.Player;
using Microsoft.OpenApi.Models;
using TGLabAPI.Infrastructure.Repositories.Transaction;
using TGLabAPI.Application.Interfaces.Repositories.Transaction;
using TGLabAPI.Application.Services.Auth;
using TGLabAPI.Application.Interfaces.Services.Auth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using TGLabAPI.Infrastructure.Service;
using TGLabAPI.Application.Interfaces.Services.Transaction;
using TGLabAPI.Application.Services.Transaction;
using TGLabAPI.Infrastructure.Middleware;
using TGLabAPI.Application.DTOs.Auth;
using TGLabAPI.Infrastructure.Services;
using System.Net.WebSockets;
using TGLabAPI.Infrastructure.WebSockets;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton<UserContext>();

#region DBContext
builder.Services.AddDbContext<ApiContext>(options =>
    options.UseNpgsql(connectionString));
#endregion

#region Repositories
builder.Services.AddScoped<IBetRepository, BetRepository>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IWalletRepository, WalletRepository>();
#endregion

#region Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBetService, BetService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddScoped<IWalletUpdateService, WalletUpdateService>();
#endregion

#region WebSocket
builder.Services.AddSingleton<WebSocketConnectionManager>();
#endregion

#region JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
#endregion

#region Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TgLab API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT no campo. Exemplo: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});
#endregion

builder.Services.AddControllers();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TgLab API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<UserContextMiddleware>();
app.MapControllers();

app.UseWebSockets();

app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocketConnectionManager = context.RequestServices.GetRequiredService<WebSocketConnectionManager>();
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var connection = webSocketConnectionManager.AddSocket(webSocket);
        await ReceiveMessages(context, webSocket, webSocketConnectionManager);
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

app.Run();

static async Task ReceiveMessages(HttpContext context, WebSocket webSocket, WebSocketConnectionManager connectionManager)
{
    var buffer = new byte[1024 * 4];
    WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

    while (!result.CloseStatus.HasValue)
    {
        var receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Console.WriteLine($"Received: {receivedMessage}");

        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
    }

    var socketId = connectionManager.GetAllSockets().FirstOrDefault(s => s.Value == webSocket).Key;
    if (socketId != null)
    {
        await connectionManager.RemoveSocket(socketId);
    }
}
