using System.ComponentModel.DataAnnotations;

namespace TGLabAPI.Application.DTOs.Player.Request
{
    public record DepositRequest(
        [Range(1, double.MaxValue, ErrorMessage = "O Valor minimo é RS1,00.")]
        double Value
    );
}
