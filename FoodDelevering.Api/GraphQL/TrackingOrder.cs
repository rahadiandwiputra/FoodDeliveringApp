namespace FoodDelevering.Api.GraphQL
{
    public record TrackingOrder
    (
        int? Id,
        int? CourierId,
        decimal Latitude,
        decimal Longitude,
        string? Status
        
    );
}
