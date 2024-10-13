using TGLabAPI.Domain.Entities;

namespace TGLabAPI.Application.DTOs.Transaction.Response
{
    public record GetBetResponse(Guid Id, double Value, string Status, Color Color, double? ValueReward, DateTimeOffset CreatedAt, double Amount);
}
