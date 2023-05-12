namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        public Task<(bool isSuccess, IEnumerable<Models.Customer> customers, string errorMessage)> GetCustomersAsync();
        public Task<(bool isSuccess, Models.Customer customer, string errorMessage)> GetCustomerAsync(int id);
    }
}
