namespace UserService.GraphQL
{
    public record CreateUser
    (
        string FullName,
        string Email,
        string UserName,
        string Password,
        int RoleId
    );
}
