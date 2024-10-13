using TgLabApi.Application.DTOs.Player.Result;
using TGLabAPI.Application.DTOs.Player.Result;
using TGLabAPI.Application.Interfaces.Repositories.Player;
using TGLabAPI.Application.Interfaces.Services.Auth;
using TGLabAPI.Application.Interfaces.Services.Player;

namespace TGLabAPI.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthService(
            IPlayerRepository playerRepository, 
            IPasswordService passwordService, 
            ITokenService tokenService, 
            IWalletService walletService
            )
        {
            _playerRepository = playerRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse?> Authenticate(string email, string password)
        {
            try
            {
                var player = await _playerRepository.GetByEmail(email);

                if (player == null) throw new ApplicationException("Usuário não encontrado.");
                if (!_passwordService.VerifyPassword(player.Password, password)) throw new Exception("A senha está errada.");

                var playerResponse = new GetPlayerResponse(player.Id, player.Name, player.Email, player.Wallet.Amount);

                var token = _tokenService.GenerateToken(player);
                if (token == null) throw new ApplicationException("Erro ao gerar o token.");

                return new LoginResponse(token, playerResponse);
            }
            catch (Exception) 
            {
                throw;
            }
        }
    }
}
