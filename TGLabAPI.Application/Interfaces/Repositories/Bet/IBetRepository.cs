using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Application.DTOs.Common;
using TGLabAPI.Application.DTOs.Transaction.Response;
using TGLabAPI.Application.Interfaces.Repositories.Common;

namespace TGLabAPI.Application.Interfaces.Repositories.Transaction
{
    public interface IBetRepository : IBaseRepository<BetEntity>
    {
        Task<List<BetEntity>> ListLastSixBets(Guid playerId);
        Task<PageableResponse<GetBetListDetailResponse>> PageableListByUser(Guid userId, int pageNumber, int pageSize);
    }
}
