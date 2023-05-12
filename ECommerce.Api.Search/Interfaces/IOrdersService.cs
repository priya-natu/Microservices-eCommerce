using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool isSuccess, IEnumerable<Order> orders, string errorMessage)> 
            GetOrdersAsync(int customerId);
    }
}
