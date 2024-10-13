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

                var walllet = await _walletService.CreateWallet(player.Id, commandCreatePlayer.Amount, commandCreatePlayer.Coin);

                return new GetPlayerResponse(player.Id, player.Name, player.Email, walllet.Amount);
            }
            catch (Exception ex) 
            {
                throw new ApplicationException($"Erro ao criar um novo jogador: {ex.Message}", ex);
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
    }
}
