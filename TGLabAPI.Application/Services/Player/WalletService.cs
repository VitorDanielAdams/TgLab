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

        public async Task<double> AddAmout(Guid walletId, double value)
        {
            try
            {
                WalletEntity? wallet = await _walletRepository.Get(walletId);
                if (wallet == null) throw new Exception("Carteira não encontrada.");

                var result = wallet.Amount + value;

                wallet.UpdateAmount(result); 
                await _walletRepository.Update(wallet);

                return wallet.Amount;
            }
            catch (Exception ex) 
            {
                throw new ApplicationException($"Erro ao adicionar valor na carteira: {ex.Message}", ex);
            }
        }

        public async Task<WalletEntity> CreateWallet(Guid playerId, double amount, string currency)
        {
            if (amount < 0) throw new Exception("O Valor da carteira não pode ser negativo");

            try
            {
                WalletEntity walletEntity = new WalletEntity(playerId, amount, currency);

                return await _walletRepository.Insert(walletEntity);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao criar uma nova carteira: {ex.Message}", ex);
            } 
        }

        public Task<WalletEntity?> GetByPlayerId(Guid playerId)
        {
            try
            {
                return _walletRepository.GetByPlayerId(playerId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao buscar carteira: {ex.Message}", ex);
            }
        }

        public async Task<double> RemoveAmout(Guid walletId, double value)
        {
            try
            {
                WalletEntity? wallet = await _walletRepository.Get(walletId);
                if (wallet == null) throw new Exception("Carteira não encontrada.");

                var result = wallet.Amount - value;

                if (result < 0) result = 0;

                wallet.UpdateAmount(result);
                await _walletRepository.Update(wallet);

                return wallet.Amount;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao adicionar valor na carteira: {ex.Message}", ex);
            }
        }

        public async Task<double> UpdateAmout(Guid walletId, double value)
        {
            try
            {
                WalletEntity? wallet = await _walletRepository.Get(walletId);
                if (wallet == null) throw new Exception("Carteira não encontrada.");

                wallet.UpdateAmount(value);
                await _walletRepository.Update(wallet);

                return wallet.Amount;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao adicionar valor na carteira: {ex.Message}", ex);
            }
        }

        public async Task<WalletEntity> UpdateWallet(WalletEntity wallet)
        {
            try
            {
                WalletEntity? dbWallet = await _walletRepository.Get(wallet.Id);
                if (dbWallet == null) throw new Exception("Carteira não encontrada.");

                dbWallet.Currency = wallet.Currency ?? dbWallet.Currency;

                await _walletRepository.Update(wallet);

                return dbWallet;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao adicionar valor na carteira: {ex.Message}", ex);
            }
        }
    }
}
