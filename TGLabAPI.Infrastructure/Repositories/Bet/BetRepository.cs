using Microsoft.EntityFrameworkCore;
using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Application.DTOs.Common;
using TGLabAPI.Application.DTOs.Transaction.Response;
using TGLabAPI.Application.Interfaces.Repositories.Transaction;
using TGLabAPI.Infrastructure.Repositories.Common;

namespace TGLabAPI.Infrastructure.Repositories.Transaction
{
    public class BetRepository : BaseRepository<BetEntity>, IBetRepository
    {
        public BetRepository(ApiContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<BetEntity>> ListLastSixBets(Guid playerId)
        {
            return await _dbContext.Set<BetEntity>()
                .Where(e => e.PlayerId == playerId)
                .OrderByDescending(e => e.CreatedAt)
                .Take(6)
                .ToListAsync();
        }

        public async Task<PageableResponse<GetBetListDetailResponse>> PageableListByUser(Guid userId, int pageNumber, int pageSize)
        {
            var context = _dbContext.Set<BetEntity>();
            var bets = await context
                .OrderBy(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var count = await context.CountAsync();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            var result = new PageableResponse<GetBetListDetailResponse>()
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalCount = totalPages,
                Result = bets.Select(e =>
                    new GetBetListDetailResponse(
                        e.Id,
                        e.Value,
                        e.Status.ToString(),
                        e.Color,
                        e.ValueReward,
                        e.CreatedAt
                    )
                ).ToList()
            };

            return result;
        }
    }
}
