using Microsoft.EntityFrameworkCore;
using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Application.DTOs.Bet.Response;
using TGLabAPI.Application.DTOs.Common;
using TGLabAPI.Application.Interfaces.Repositories.Transaction;
using TGLabAPI.Infrastructure.Repositories.Common;

namespace TGLabAPI.Infrastructure.Repositories.Transaction
{
    public class TransactionRepository : BaseRepository<TransactionEntity>, ITransactionRepository
    {
        public TransactionRepository(ApiContext dbContext) : base(dbContext)
        {
        }

        public async Task<PageableResponse<GetTransactionResponse>> PageableListByUser(Guid userId, int pageNumber, int pageSize)
        {
            var context = _dbContext.Set<TransactionEntity>();
            var bets = await context
                .OrderBy(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await context.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var result = new PageableResponse<GetTransactionResponse>()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalCount = totalPages,
                Result = bets.Select(e => new GetTransactionResponse(e.Id, e.CreatedAt, e.Type.ToString(), e.Value)).ToList()
            };

            return result; 
        }
    }
}
