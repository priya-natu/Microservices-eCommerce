using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ILogger<OrdersService> logger;

        public OrdersService(IHttpClientFactory clientFactory, ILogger<OrdersService> logger) 
        {
            this.clientFactory = clientFactory;
            this.logger = logger;
        }

        public ILogger<OrdersService> Logger { get; }

        public async Task<(bool isSuccess, IEnumerable<Order> orders, string errorMessage)> 
            GetOrdersAsync(int customerId)
        {
			try
			{
                var client = clientFactory.CreateClient("OrdersService");
                var response = await client.GetAsync($"api/orders/{customerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var orders = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
                    return (true, orders, null);
                }
                return (false, null, "not found");
			}
			catch (Exception ex)
			{
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
