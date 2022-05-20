using FoodDeleveryApp.Data.Models;
using HotChocolate.AspNetCore.Authorization;

namespace FoodDelevering.Api.GraphQL
{
    public class Query
    {
        [Authorize]
        public IQueryable<Order> GetAllOrders([Service] FoodDeliveryContext context) =>
              context.Orders;
        [Authorize]
        public IQueryable<Product> GetAllProduct([Service] FoodDeliveryContext context) =>
              context.Products;

    }
}
