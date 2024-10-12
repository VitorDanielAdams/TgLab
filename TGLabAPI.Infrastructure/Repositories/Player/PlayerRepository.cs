using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
