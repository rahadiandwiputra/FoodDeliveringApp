namespace FoodDelevering.Api.GraphQL
{
    public record ProductInput
    (
        int? Id,
        string Name,
        int Stock,
        double Price,
        int CategoryId
    );
}
