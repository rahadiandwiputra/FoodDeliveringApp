namespace UserService.GraphQL
{
    public record UpdateUser
    (
        int Id,
        string FullName,
        string Email,
        string UserName,
        string Password
    );
}
