using AutoMapper;
using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider 
    {
        private readonly ProductsDBContext dBContext;
        private readonly IMapper mapper;
        private readonly ILogger<ProductsProvider> logger;
        public ProductsProvider(ProductsDBContext dBContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dBContext = dBContext;
            this.mapper = mapper;
            this.logger = logger;

            SeedData();
        }

        private void SeedData()
        {
            if (!dBContext.Products.Any())
            {
                dBContext.Products.Add(new DB.Product { Id = 1, Name = "Keyboard", Price = 30, Inventory = 100 });
                dBContext.Products.Add(new DB.Product { Id = 2, Name = "Mouse", Price = 20, Inventory = 100 });
                dBContext.Products.Add(new DB.Product { Id = 3, Name = "Monitor", Price = 150, Inventory = 100 });
                dBContext.Products.Add(new DB.Product { Id = 4, Name = "CPU", Price = 200, Inventory = 100 });
                dBContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var result = await dBContext.Products.ToListAsync();
                if (result != null && result.Any())
                {
                    var products = mapper.Map<IEnumerable<Models.Product>>(result);
                    return (true, products, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Product products, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var result = await dBContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (result != null)
                {
                    var product = mapper.Map<DB.Product, Models.Product>(result);
                    return (true, product, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
