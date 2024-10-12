using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Application.Interfaces.Repositories.Transaction;
using TGLabAPI.Infrastructure.Repositories.Common;

namespace TGLabAPI.Infrastructure.Repositories.Transaction
{
    public class TransactionRepository : BaseRepository<TransactionEntity>, ITransactionRepository
    {
        public TransactionRepository(ApiContext dbContext) : base(dbContext)
        {
        }
    }
}
