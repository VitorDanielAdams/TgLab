using System.ComponentModel.DataAnnotations;

namespace TgLabApi.Application.DTOs.Player.Request
{
    public record LoginRequest(
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        string Email,

        [Required(ErrorMessage = "A senha é obrigatória.")]
        string Password
    );
}
