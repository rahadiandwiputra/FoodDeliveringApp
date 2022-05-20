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
        /*---------------------------------- ORDER API ----------------------------------*/
        /*[Authorize(Roles = new[] { "BUYER" })]
        public async Task<Order> AddOrderAsync(
            )
        {

        }*/
        /*---------------------------------- CRUD COURIER ----------------------------------*//*
        [Authorize(Roles = new[] { "MANAGER" })]
        public async Task<Courier> AddCourierAsync(
            CourierInput input,
            [Service] FoodDeliveryContext context)
        {

            // EF
            var courier = new Courier
            {
                Name = input.Name,
                Address = input.Address,
                City = input.City,
                Phone = input.Phone,
                Completed = input.Completed,
            };

            var ret = context.Couriers.Add(courier);
            await context.SaveChangesAsync();

            return ret.Entity;
        }

        [Authorize(Roles = new[] { "MANAGER" })]
        public async Task<Courier> GetCourierByIdAsync(
            int id,
            [Service] FoodDeliveryContext context)
        {
            var courier = context.Couriers.Where(o => o.Id == id).FirstOrDefault();

            return await Task.FromResult(courier);
        }

        [Authorize(Roles = new[] { "MANAGER" })]
        public async Task<Courier> UpdateCourierAsync(
            CourierInput input,
            [Service] FoodDeliveryContext context)
        {
            var courier = context.Couriers.Where(o => o.Id == input.Id).FirstOrDefault();
            if (courier != null)
            {
                courier.Name = input.Name;
                courier.Address = input.Address;
                courier.City = input.City;
                courier.Phone = input.Phone;
                courier.Completed = input.Completed;

                context.Couriers.Update(courier);
                await context.SaveChangesAsync();
            }


            return await Task.FromResult(courier);
        }

        [Authorize(Roles = new[] { "MANAGER" })]
        public async Task<Courier> DeleteCourierByIdAsync(
            int id,
            [Service] FoodDeliveryContext context)
        {
            var courier = context.Couriers.Where(o => o.Id == id).FirstOrDefault();
            if (courier != null)
            {
                context.Couriers.Remove(courier);
                await context.SaveChangesAsync();
            }
            return await Task.FromResult(courier);
        }*/
    }
}
