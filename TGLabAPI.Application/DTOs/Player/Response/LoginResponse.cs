using TgLabApi.Application.DTOs.Player.Result;

namespace TGLabAPI.Application.DTOs.Player.Result
{
    public record LoginResponse(string Token, GetPlayerResponse Player);
}