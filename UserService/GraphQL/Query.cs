using FoodDeleveryApp.Data.Models;
using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;

namespace UserService.GraphQL
{
    public class Query
    {
        
        [Authorize]
        public IQueryable<UserData> GetUsersCondition([Service] FoodDeliveryContext context, ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Identity.Name;
            // check manager role ?
            var adminRole = claimsPrincipal.Claims.Where(o => o.Type == ClaimTypes.Role && o.Value == "ADMIN").FirstOrDefault();
            var user = context.Users.Where(o => o.Username == userName).FirstOrDefault();
            if (user != null)
            {
                if (adminRole != null)
                {
                    return context.Users.Select(p => new UserData()
                    {
                        Id = p.Id,
                        FullName = p.FullName,
                        Email = p.Email,
                        Username = p.Username
                    });
                }
                else
                {
                     var u = context.Users.Where(o => o.Id == user.Id).Select(p=>new UserData()
                     {
                         Id=p.Id,
                         FullName=p.FullName,
                         Email=p.Email, 
                         Username=p.Username
                     });
                    return u;
                }
            }
            return context.Users.Select(p => new UserData()
            {
                FullName = "",
            });
        }
        [Authorize]
        public IQueryable<Profile> GetProfileCondition([Service] FoodDeliveryContext context, ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Identity.Name;
            // check manager role ?
            var user = context.Users.Where(o => o.Username == userName).FirstOrDefault();

            var profiles = context.Profiles.Where(o => o.UserId == user.Id);
            return profiles.AsQueryable();
        }

    }
}
