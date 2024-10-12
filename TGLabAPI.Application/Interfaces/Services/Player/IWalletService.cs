using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Application.DTOs.Player.Request;
using TgLabApi.Domain.Entities.Player;

namespace TGLabAPI.Application.Interfaces.Services.Player
{
    public interface IWalletService
    {
        Task<WalletEntity> CreateWallet(Guid playerId, double amount, string coin);
    }
}
