using Microsoft.EntityFrameworkCore;
using TgLabApi.Domain.Entities.Player;
using TGLabAPI.Application.Interfaces.Repositories.Player;
using TGLabAPI.Infrastructure.Repositories.Common;

namespace TGLabAPI.Infrastructure.Repositories.Player
{
    public class WalletRepository : BaseRepository<WalletEntity>, IWalletRepository
    {
        public WalletRepository(ApiContext dbContext) : base(dbContext)
        {
        }

        public async Task<WalletEntity?> GetByPlayerId(Guid playerId)
        {
            return await _dbContext.Set<WalletEntity>()
                .Where(e => e.PlayerId == playerId)
                .FirstOrDefaultAsync();
        }
    }
}
