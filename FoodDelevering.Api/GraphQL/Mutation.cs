using FoodDeleveryApp.Data.Models;
using HotChocolate.AspNetCore.Authorization;

namespace FoodDelevering.Api.GraphQL
{
    public class Mutation
    {
        /*---------------------------------- CRUD PRODUCT ----------------------------------*/
        [Authorize(Roles = new[] { "MANAGER" })]
        public async Task<Product> AddProductAsync(
            ProductInput input,
            [Service] FoodDeliveryContext context)
        {

            // EF
            var product = new Product
            {
                Name = input.Name,
                Stock = input.Stock,
                Price = input.Price,
                Created = DateTime.Now,
                CategoryId = input.CategoryId,
            };

            var ret = context.Products.Add(product);
            await context.SaveChangesAsync();

            return ret.Entity;
        }

        [Authorize(Roles = new[] { "MANAGER" })]
        public async Task<Product> GetProductByIdAsync(
            int id,
            [Service] FoodDeliveryContext context)
        {
            var product = context.Products.Where(o => o.Id == id).FirstOrDefault();

            return await Task.FromResult(product);
        }

        [Authorize(Roles = new[] { "MANAGER" })]
        public async Task<Product> UpdateProductAsync(
            ProductInput input,
            [Service] FoodDeliveryContext context)
        {
            var product = context.Products.Where(o => o.Id == input.Id).FirstOrDefault();
            if (product != null)
            {
                product.Name = input.Name;
                product.Stock = input.Stock;
                product.Price = input.Price;
                product.CategoryId = input.CategoryId;

                context.Products.Update(product);
                await context.SaveChangesAsync();
            }


            return await Task.FromResult(product);
        }

        [Authorize(Roles = new[] { "MANAGER" })]
        public async Task<Product> DeleteProductByIdAsync(
            int id,
            [Service] FoodDeliveryContext context)
        {
            var product = context.Products.Where(o => o.Id == id).FirstOrDefault();
            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
            return await Task.FromResult(product);
        }
    }
}
