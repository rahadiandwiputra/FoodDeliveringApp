using FoodDeleveryApp.Data.Models;
using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;

namespace FoodDelevering.Api.GraphQL
{
    public class Mutation
    {
        /*---------------------------------- CRUD PRODUCT ----------------------------------*/
        [Authorize(Roles = new[] { "MANAGER" })]
        public async Task<Product> AddProductAsync(
            FoodInput input,
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
            FoodInput input,
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
        [Authorize(Roles = new[] { "BUYER" })]
        public async Task<OrderOutput> AddOrderAsync(
            OrderData input, [Service] FoodDeliveryContext context, ClaimsPrincipal claimsPrincipal)
        {
            using var transaction = context.Database.BeginTransaction();

            var userName = claimsPrincipal.Identity.Name;
            try
            {
                var user = context.Users.Where(o=>o.Username == userName).FirstOrDefault();

                if (user != null)
                {
                    Order order = new Order
                    {
                        Code = input.Code,
                        UserId = user.Id,
                        Status = "ON PROCESS",
                    };
                    context.Orders.Add(order);

                    foreach (var item in input.Details)
                    {
                        OrderDetail detail = new OrderDetail
                        {
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                        };
                        order.OrderDetails.Add(detail);
                    }
                    
                    context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new OrderOutput
                    {
                        TransactionDate = DateTime.Now.ToString(),
                        Message = "Successfully made order data!"
                    };
                }
                else
                {
                    return new OrderOutput
                    {
                        TransactionDate = DateTime.Now.ToString(),
                        Message = "User Not Found"
                    };
                }
            }catch (Exception ex)
            {
                transaction.Rollback();
                return new OrderOutput
                {
                    TransactionDate = DateTime.Now.ToString(),
                    Message = ex.Message
                };
            }
        }
        [Authorize(Roles = new [] { "COURIER" })]
        public async Task<OrderOutput> AddTrackingOrderAsync(
            TrackingOrder input, [Service] FoodDeliveryContext context, ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                var userName = claimsPrincipal.Identity.Name;
                var user = context.Users.Where(o=>o.Username == userName).FirstOrDefault();
                var courier = context.Couriers.Where(o=>o.UserId == user.Id).FirstOrDefault();
                var orderId = context.Orders.Where(o=>o.Id == input.Id).FirstOrDefault();
                //add tracking 
                if (user!=null)
                {
                    if (courier!=null)
                    {
                        if (orderId!=null)
                        {
                            orderId.CourierId = courier.Id;
                            orderId.Latitude = input.Latitude;
                            orderId.Longitude = input.Longitude;
                            orderId.Status = "ON DELIVERY";
                            context.Orders.Update(orderId);
                            context.SaveChangesAsync();
                            return new OrderOutput
                            {
                                TransactionDate = DateTime.Now.ToString(),
                                Message = "Pesanan Sedang Dalam Perjalanan"
                            };
                        }
                        else
                        {
                            return new OrderOutput
                            {
                                TransactionDate = DateTime.Now.ToString(),
                                Message = "Order NotFound"
                            };
                        }

                    }
                    else
                    {
                        return new OrderOutput
                        {
                            TransactionDate = DateTime.Now.ToString(),
                            Message = "Courier NotFound"
                        };
                    }
                }
                else
                {
                    return new OrderOutput
                    {
                        TransactionDate = DateTime.Now.ToString(),
                        Message = "User NotFound"
                    };
                }
            }
            catch (Exception ex)
            {
                return new OrderOutput
                {
                    TransactionDate = DateTime.Now.ToString(),
                    Message = ex.Message
                };
            }
        }
        [Authorize(Roles = new[] { "COURIER" })]
        public async Task<OrderOutput> CompleteOrderAsync(
            int id, [Service] FoodDeliveryContext context, ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                var order = context.Orders.Where(o => o.Id == id).FirstOrDefault();
                //order completed
                if (order != null)
                {
                    order.Status = "COMPLETED";
                    context.Orders.Update(order);
                    context.SaveChangesAsync();
                    return new OrderOutput
                    {
                        TransactionDate = DateTime.Now.ToString(),
                        Message = "Pesanan Selesai Dikirim"
                    };
                }
                return new OrderOutput
                {
                    TransactionDate = DateTime.Now.ToString(),
                    Message = "Kurir Tidak Ditemukan"
                };
            }
            catch (Exception ex)
            {
                return new OrderOutput
                {
                    TransactionDate = DateTime.Now.ToString(),
                    Message = ex.Message
                };
            }
        }

    }
}
