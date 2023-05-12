using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory clientFactory;
        private readonly ILogger<ProductsService> logger;

        public ProductsService(IHttpClientFactory clientFactory, ILogger<ProductsService> logger)
        {
            this.clientFactory = clientFactory;
            this.logger = logger;
        }
        public async Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync()
        {
            try
            {
                var client = clientFactory.CreateClient("ProductsService");
                var result = await client.GetAsync($"api/products");
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var products = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);
                    return (true, products, null);
                }
                return (false, null, result.ReasonPhrase);
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
