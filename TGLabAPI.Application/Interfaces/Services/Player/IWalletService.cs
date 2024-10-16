﻿using System;
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
        Task<WalletEntity> CreateWallet(Guid playerId, double amount, string currency);
        Task<WalletEntity?> GetByPlayerId(Guid playerId);
        Task<double> RemoveAmout(Guid walletId, double value);
        Task<double> AddAmout(Guid walletId, double value); 
        Task<double> UpdateAmout(Guid walletId, double value);
        Task<WalletEntity> UpdateWallet(WalletEntity wallet);
    }
}
