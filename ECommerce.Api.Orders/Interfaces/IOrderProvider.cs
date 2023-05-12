namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrderProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.Order> orders, string errorMessage)> GetOrderAsync(int customerId);
    }
}
