using TgLabApi.Application.DTOs.Player.Request;
using TgLabApi.Application.DTOs.Player.Result;
using TgLabApi.Domain.Entities.Player;
using TGLabAPI.Application.DTOs.Auth;
using TGLabAPI.Application.DTOs.Player.Request;
using TGLabAPI.Application.Interfaces.Repositories.Player;
using TGLabAPI.Application.Interfaces.Services.Auth;
using TGLabAPI.Application.Interfaces.Services.Player;

namespace TGLabAPI.Application.Services.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPasswordService _passwordService;
        private readonly IWalletService _walletService;
        private readonly UserContext _userContext;

        public PlayerService(
            IPlayerRepository playerRepository, 
            IPasswordService passwordService, 
            IWalletService walletService, 
            UserContext userContext
            )
        {
            _playerRepository = playerRepository;
            _passwordService = passwordService;
            _walletService = walletService;
            _userContext = userContext;
        }

        public async Task<GetPlayerResponse> CreatePlayer(CreatePlayerRequest commandCreatePlayer)
        {
            try
            {
                var hashedPassword = _passwordService.HashPassword(commandCreatePlayer.Password);

                PlayerEntity player = new PlayerEntity(commandCreatePlayer.Name, commandCreatePlayer.Email, hashedPassword);

                await _playerRepository.Insert(player);

                var walllet = await _walletService.CreateWallet(player.Id, commandCreatePlayer.Amount, commandCreatePlayer.Currency);

                return new GetPlayerResponse(player.Id, player.Name, player.Email, walllet.Amount);
            }
            catch (Exception ex) 
            {
                throw new ApplicationException($"Erro ao criar um novo jogador: {ex.Message}", ex);
            }
        }

        public async Task DeleteAccount()
        {
            try
            {
                var userId = _userContext.Id;

                var player = await _playerRepository.Get(userId);
                if (player == null) throw new Exception("Usuário não encontrado.");

                await _playerRepository.Delete(player);
                _userContext.Clean();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao fazer depósito: {ex.Message}", ex);
            }
        }

        public async Task<GetPlayerResponse?> Deposit(DepositRequest request)
        {
            try
            {
                var userId = _userContext.Id;

                var player = await _playerRepository.Get(userId);
                if (player == null) throw new Exception("Usuário não encontrado.");

                var result = await _walletService.AddAmout(player.Wallet.Id, request.Value);
                player.Wallet.UpdateAmount(result);

                return new GetPlayerResponse(player.Id, player.Name, player.Email, player.Wallet.Amount);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao fazer depósito: {ex.Message}", ex);
            }
        }

        public async Task<PlayerEntity?> Me()
        {
            try
            {
                var userId = _userContext.Id;

                return await _playerRepository.Get(userId);
            }
            catch (Exception ex) 
            {
                throw new ApplicationException($"Erro ao buscar jogador: {ex.Message}", ex);
            }
        }

        public async Task<PlayerEntity?> Update(PlayerEntity player)
        {
            try
            {
                var dbPlayer = await _playerRepository.Get(player.Id);
                if (dbPlayer == null) throw new Exception("Usuário não encontrado.");

                dbPlayer.Name = player.Name ?? dbPlayer.Name;
                dbPlayer.Email = player.Email ?? dbPlayer.Email;
                dbPlayer.LastBonusDate = player.LastBonusDate ?? dbPlayer.LastBonusDate;
                dbPlayer.Wallet.Currency = player.Wallet.Currency ?? dbPlayer.Wallet.Currency;

                await _walletService.UpdateWallet(dbPlayer.Wallet);

                await _playerRepository.Update(dbPlayer);

                return dbPlayer;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao fazer depósito: {ex.Message}", ex);
            }
        }

        public async Task<GetPlayerResponse?> UpdateAccount(UpdatePlayerRequest request)
        {
            try
            {
                var userId = _userContext.Id;

                var player = await _playerRepository.Get(userId);
                if (player == null) throw new Exception("Usuário não encontrado.");

                player.Name = request.Name ?? player.Name;
                player.Email = request.Email ?? player.Email;
                player.Wallet.Currency = request.Currency ?? player.Wallet.Currency;

                await _walletService.UpdateWallet(player.Wallet);
                
                await _playerRepository.Update(player);

                return new GetPlayerResponse(player.Id, player.Name, player.Email, player.Wallet.Amount);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Erro ao fazer depósito: {ex.Message}", ex);
            }
        }
    }
}
