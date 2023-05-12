using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync();
        
        Task<(bool IsSuccess, Product products, string ErrorMessage)> GetProductAsync(int id);
    }
}
