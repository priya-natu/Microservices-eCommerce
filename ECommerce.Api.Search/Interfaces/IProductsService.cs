using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync();
    }
}
