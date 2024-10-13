
using System.ComponentModel.DataAnnotations;
using TGLabAPI.Domain.Entities;

namespace TGLabAPI.Application.DTOs.Transaction.Request
{
    public record CreateBetRequest(
        [Range(1, double.MaxValue, ErrorMessage = "O Valor minimo de aposta é RS1,00.")]
        double Value,
        [EnumDataType(typeof(Color), ErrorMessage = "Tipo de aposta inválido.")]
        Color Color
    );
}
