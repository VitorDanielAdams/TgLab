using TGLabAPI.Application.Interfaces.Repositories.Player;
using TGLabAPI.Application.Interfaces.Services.Auth;

namespace TGLabAPI.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AuthService(IPlayerRepository playerRepository, IPasswordService passwordService, ITokenService tokenService)
        {
            _playerRepository = playerRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<string?> Authenticate(string email, string password)
        {
            var player = await _playerRepository.GetByEmail(email);

            if (player == null) throw new Exception("Usuário não encontrado.");
            if (!_passwordService.VerifyPassword(player.Password, password)) throw new Exception("A senha está errada.");

            return _tokenService.GenerateToken(player);
        }
    }
}
