using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgLabApi.Application.DTOs.Player.Request;
using TgLabApi.Application.DTOs.Player.Result;
using TgLabApi.Domain.Entities.Player;
using TGLabAPI.Application.Interfaces.Repositories.Player;
using TGLabAPI.Application.Interfaces.Services.Player;

namespace TGLabAPI.Application.Services.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPasswordService _passwordService;

        public PlayerService(IPlayerRepository playerRepository, IPasswordService passwordService)
        {
            _playerRepository = playerRepository;
            _passwordService = passwordService;
        }

        public async Task<GetPlayerResult> CreatePlayer(CreatePlayerRequest newPlayer)
        {
            if (newPlayer == null)
            {
                throw new ArgumentNullException(nameof(newPlayer), "Requisição inválida para criação de jogador.");
            }

            try
            {
                var hashedPassword = _passwordService.HashPassword(newPlayer.Password);

                PlayerEntity player = new PlayerEntity(newPlayer.Name, newPlayer.Email, hashedPassword, 0);

                await _playerRepository.Insert(player);

                WalletEntity walletEntity = new WalletEntity(player.Id, newPlayer.Amount, newPlayer.Coin);

                return new GetPlayerResult(player.Name, player.Email, walletEntity.Amount);
            }
            catch (Exception ex) 
            {
                throw new ApplicationException("Erro ao criar um novo jogador", ex);
            }
        }
    }
}
