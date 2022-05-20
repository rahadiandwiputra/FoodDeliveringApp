using FoodDeleveryApp.Data.Models;
using HotChocolate.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FoodDelevering.Api.GraphQL
{
    public class Query
    {
        [Authorize(Roles = new [] {"MANAGER", "BUYER"})]
        public IQueryable<Order> GetAllOrders([Service] FoodDeliveryContext context, ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Identity.Name;
            var managerRole = claimsPrincipal.Claims.Where(o => o.Type == ClaimTypes.Role && o.Value == "MANAGER").FirstOrDefault();
            var user = context.Users.Where(o => o.Username == userName).FirstOrDefault();
            if (user != null)
            {
                if (managerRole != null)
                {
                    return context.Orders.Include(o => o.OrderDetails);
                }
                var order = context.Orders.Where(o=>o.UserId == user.Id).Include(o=>o.OrderDetails);
                return order.AsQueryable();
            }
            return new List<Order>().AsQueryable();
        }
        [Authorize(Roles =new[] { "MANAGER", "BUYER"})]
        public IQueryable<Product> GetAllProduct([Service] FoodDeliveryContext context) =>
              context.Products.Include(o=>o.Category);

    }
}
