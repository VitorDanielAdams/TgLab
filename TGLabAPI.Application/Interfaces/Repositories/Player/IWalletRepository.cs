using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Domain.Entities.Player;
using TGLabAPI.Application.Interfaces.Repositories.Common;

namespace TGLabAPI.Application.Interfaces.Repositories.Player
{
    public interface IWalletRepository : IBaseRepository<WalletEntity>
    {
        Task<WalletEntity?> GetByPlayerId(Guid playerId);
    }
}
