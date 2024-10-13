using TGLabAPI.Domain.Entities;

namespace TGLabAPI.Application.DTOs.Transaction.Response
{
    public record GetBetListDetailResponse(Guid Id, double Value, string Status, Color Color, double? ValueReward, DateTimeOffset CreatedAt);
}
