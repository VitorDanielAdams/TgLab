using TgLabApi.Domain.Entities.Transaction;
using TGLabAPI.Application.Interfaces.Repositories.Transaction;
using TGLabAPI.Infrastructure.Repositories.Common;

namespace TGLabAPI.Infrastructure.Repositories.Transaction
{
    public class BetRepository : BaseRepository<BetEntity>, IBetRepository
    {
        public BetRepository(ApiContext dbContext) : base(dbContext)
        {
        }
    }
}
