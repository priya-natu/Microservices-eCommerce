using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;

        public SearchService(IOrdersService ordersService, IProductsService productsService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
        }
        public async Task<(bool isSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var orderResult = await ordersService.GetOrdersAsync(customerId);
            var productResult = await productsService.GetProductsAsync();

            if (orderResult.isSuccess)
            {

                foreach (var order in orderResult.orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.products.FirstOrDefault(x => x.Id == item.ProductId).Name;
                    }
                }

                var result = new
                {
                    Orders = orderResult.orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
