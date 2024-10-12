using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Domain.Entities.Player;
using TGLabAPI.Application.Interfaces.Repositories.Player;
using TGLabAPI.Application.Interfaces.Services.Player;

namespace TGLabAPI.Application.Services.Player
{
    public class WalletService : IWalletService
    {
        private readonly IWalletRepository _walletRepository;

        public WalletService(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<WalletEntity> CreateWallet(Guid playerId, double amount, string coin)
        {
            if (amount < 0) throw new Exception("O Valor da carteira não pode ser negativo");

            try
            {
                WalletEntity walletEntity = new WalletEntity(playerId, amount, coin);

                return await _walletRepository.Insert(walletEntity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao criar uma nova carteira: {ex.Message}", ex);
            } 
        }
    }
}
