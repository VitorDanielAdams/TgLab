using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Application.DTOs.Player.Request;
using TgLabApi.Application.DTOs.Player.Result;
using TgLabApi.Domain.Entities.Player;
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

        public PlayerService(IPlayerRepository playerRepository, IPasswordService passwordService, IWalletService walletService)
        {
            _playerRepository = playerRepository;
            _passwordService = passwordService;
            _walletService = walletService;
        }

        public async Task<GetPlayerResult> CreatePlayer(CreatePlayerRequest commandPlayer)
        {
            if (commandPlayer == null)
            {
                throw new ArgumentNullException(nameof(commandPlayer), "Requisição inválida para criação de jogador.");
            }

            try
            {
                var hashedPassword = _passwordService.HashPassword(commandPlayer.Password);

                PlayerEntity player = new PlayerEntity(commandPlayer.Name, commandPlayer.Email, hashedPassword, 0);

                await _playerRepository.Insert(player);

                var walllet = await _walletService.CreateWallet(player.Id, commandPlayer.Amount, commandPlayer.Coin);

                return new GetPlayerResult(player.Name, player.Email, walllet.Amount);
            }
            catch (Exception ex) 
            {
                throw new ApplicationException($"Erro ao criar um novo jogador: {ex.Message}", ex);
            }
        }
    }
}
