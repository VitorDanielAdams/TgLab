using TgLabApi.Domain.Entities.Player;
using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Application.DTOs.Common;
using TGLabAPI.Application.DTOs.Transaction.Request;
using TGLabAPI.Application.DTOs.Transaction.Response;

namespace TGLabAPI.Application.Interfaces.Services.Transaction
{
    public interface IBetService
    {
        Task<GetBetResponse> CreateBet(CreateBetRequest request);
        Task<GetBetResponse> CancelBet(Guid id);
        Task<BetEntity> ProcessBet(BetEntity bet, PlayerEntity player, Guid walletId);
        Task<PageableResponse<GetBetListDetailResponse>> List(PageableRequest request);
    }
}
