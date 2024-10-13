using System.ComponentModel.DataAnnotations;

namespace TGLabAPI.Application.DTOs.Player.Request
{
    public record UpdatePlayerRequest(
        string? Name,
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        string? Email,
        string? Currency = "R$ - BRL"
    );
}
