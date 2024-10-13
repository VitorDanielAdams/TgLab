
using TGLabAPI.Domain.Entities;

namespace TGLabAPI.Application.DTOs.Bet.Response
{
    public record GetTransactionResponse(Guid Id, DateTimeOffset CreatedAt, string Type, double Value);
}
