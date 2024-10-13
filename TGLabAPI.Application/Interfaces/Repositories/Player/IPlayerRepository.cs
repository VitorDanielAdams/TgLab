using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Application.DTOs.Player.Result;
using TgLabApi.Domain.Entities.Player;
using TGLabAPI.Application.Interfaces.Repositories.Common;

namespace TGLabAPI.Application.Interfaces.Repositories.Player
{
    public interface IPlayerRepository : IBaseRepository<PlayerEntity>
    {
        Task<PlayerEntity?> Get(Guid id);
        Task<PlayerEntity?> GetByEmail(string email);
    }
}
