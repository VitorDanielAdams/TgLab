using Microsoft.AspNetCore.Mvc;
using TgLabApi.Application.DTOs.Player.Request;
using TGLabAPI.Application.DTOs.Player.Result;
using TGLabAPI.Application.Interfaces.Services.Auth;

namespace TgLabApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (request == null || !ModelState.IsValid) return BadRequest("Dados de login incompletos.");

                var token = await _authService.Authenticate(request.Email, request.Password);

                if (token == null) return Unauthorized("Credenciais inválidas.");

                return Ok(new LoginResult(token));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }
    }
}
