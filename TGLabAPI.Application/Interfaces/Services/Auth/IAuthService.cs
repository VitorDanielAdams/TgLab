using TGLabAPI.Application.DTOs.Player.Result;

namespace TGLabAPI.Application.Interfaces.Services.Auth
{
    public interface IAuthService
    {
        Task<LoginResponse?> Authenticate(string email, string password);
    }
}