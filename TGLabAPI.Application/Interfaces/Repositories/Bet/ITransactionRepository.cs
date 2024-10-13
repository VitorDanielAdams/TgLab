using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Application.DTOs.Bet.Response;
using TGLabAPI.Application.DTOs.Common;
using TGLabAPI.Application.Interfaces.Repositories.Common;

namespace TGLabAPI.Application.Interfaces.Repositories.Transaction
{
    public interface ITransactionRepository : IBaseRepository<TransactionEntity>
    {
        Task<PageableResponse<GetTransactionResponse>> PageableListByUser(Guid userId, int pageNumber, int pageSize);
    }
}
