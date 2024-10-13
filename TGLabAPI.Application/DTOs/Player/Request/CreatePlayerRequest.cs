using System.ComponentModel.DataAnnotations;

namespace TgLabApi.Application.DTOs.Player.Request
{
    public record CreatePlayerRequest(
        [Required(ErrorMessage = "O nome é obrigatório.")]
        string Name,

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail não é válido.")]
        string Email,

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        string Password,

        [Required(ErrorMessage = "O valor da carteira é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor da carteira deve ser um número positivo.")]
        double Amount = 0,

        [Required(ErrorMessage = "A moeda é obrigatória.")]
        string Coin = "R$ - BRL"
    );
}
