using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Application.DTOs.Bet.Response;
using TGLabAPI.Application.DTOs.Common;
using TGLabAPI.Domain.Entities;

namespace TGLabAPI.Application.Interfaces.Services.Transaction
{
    public interface ITransactionService
    {
        Task<TransactionEntity> CreateTransaction(Guid walletId, Guid betId, double value, TransactionType type);
        Task<PageableResponse<GetTransactionResponse>> List(PageableRequest request);
    }
}
