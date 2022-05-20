namespace FoodDelevering.Api.GraphQL
{
    public record OrderInput
    (
        string Code,
        int UserId,
        string Status
    );
}
