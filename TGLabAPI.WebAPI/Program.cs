using Microsoft.EntityFrameworkCore;
using TGLabAPI.Infrastructure;
using TGLabAPI.Infrastructure.Repositories.Player;
using TGLabAPI.Application.Interfaces.Repositories.Player;
using TGLabAPI.Application.Interfaces.Services.Player;
using TGLabAPI.Application.Services.Player;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApiContext>(options =>
    options.UseNpgsql(connectionString));

#region Repositories
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
#endregion

#region Services.
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
#endregion

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TgLab API", Version = "v1" });
});

builder.Services.AddControllers();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllers();

app.Run();