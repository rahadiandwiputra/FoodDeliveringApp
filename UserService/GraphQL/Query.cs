using FoodDeleveryApp.Data.Models;
using HotChocolate.AspNetCore.Authorization;

namespace UserService.GraphQL
{
    public class Query
    {
        [Authorize] // dapat diakses kalau sudah login
        public IQueryable<UserData> GetUsers([Service] FoodDeliveryContext context) =>
            context.Users.Select(p => new UserData()
            {
                Id = p.Id,
                FullName = p.FullName,
                Email = p.Email,
                Username = p.Username
            });
    }
}
