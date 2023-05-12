using AutoMapper;
using ECommerce.Api.Orders.DB;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Provider
{
    public class OrderProvider : IOrderProvider
    {
        private readonly OrdersDbContext ordersDbContext;
        private readonly IMapper mapper;
        private readonly ILogger<OrderProvider> logger;

        public OrderProvider(OrdersDbContext ordersDbContext, IMapper mapper, ILogger<OrderProvider> logger)
        {
            this.ordersDbContext = ordersDbContext;
            this.mapper = mapper;
            this.logger = logger;

            SeedData();
        }

        private void SeedData()
        {
            if (!ordersDbContext.Orders.Any())
            {
                ordersDbContext.Orders.Add(new DB.Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Items = new List<DB.OrderItem>()
                    {
                        new DB.OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                ordersDbContext.Orders.Add(new DB.Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Items = new List<DB.OrderItem>()
                    {
                        new DB.OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                ordersDbContext.Orders.Add(new DB.Order()
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Items = new List<DB.OrderItem>()
                    {
                        new DB.OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new DB.OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                ordersDbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Order> orders, string errorMessage)> GetOrderAsync(int customerId)
        {
			try
			{
                var result = await ordersDbContext.Orders
                    .Where(x => x.CustomerId == customerId)
                    .Include(o => o.Items)
                    .ToListAsync();
                
                if (result != null && result.Any())
                {
                    var order = mapper.Map<IEnumerable<DB.Order>, IEnumerable<Models.Order>>(result);
                    return (true, order, null);
                }
                return (false, null, "not found");

            }
            catch (Exception ex)
			{
                logger? .LogError(ex.ToString());
                return (false, null, ex.Message);
			}
        }
    }
}
