using TgLabApi.Application.DTOs.Player.Request;
using TgLabApi.Application.DTOs.Player.Result;
using TgLabApi.Domain.Entities.Player;
using TGLabAPI.Application.DTOs.Player.Request;

namespace TGLabAPI.Application.Interfaces.Services.Player
{
    public interface IPlayerService
    {
        Task<GetPlayerResponse> CreatePlayer(CreatePlayerRequest request);
        Task<PlayerEntity?> Me();
        Task<GetPlayerResponse?> Deposit(DepositRequest request);
    }
}
