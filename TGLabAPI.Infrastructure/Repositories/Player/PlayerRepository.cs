using Microsoft.EntityFrameworkCore;
using TgLabApi.Domain.Entities.Player;
using TGLabAPI.Application.Interfaces.Repositories.Player;
using TGLabAPI.Infrastructure.Repositories.Common;

namespace TGLabAPI.Infrastructure.Repositories.Player
{
    public class PlayerRepository : BaseRepository<PlayerEntity>, IPlayerRepository
    {
        public PlayerRepository(ApiContext dbContext) : base(dbContext)
        {
        }

        public async Task<PlayerEntity?> Get(Guid id)
        {
            return await _dbContext.Set<PlayerEntity>()
                .Where(e => e.Id == id)
                .Include(e => e.Wallet)
                .FirstOrDefaultAsync();
        }

        public async Task<PlayerEntity?> GetByEmail(string email)
        {
            return await _dbContext.Set<PlayerEntity>()
                .Where(e => e.Email == email)
                .Include(e => e.Wallet)
                .FirstOrDefaultAsync();
        }
    }
}
