namespace FoodDelevering.Api.GraphQL
{
    public record CourierInput
    (
        int? Id,
        string Name,
        string Address,
        string City,
        string Phone,
        bool Completed
    );

}
