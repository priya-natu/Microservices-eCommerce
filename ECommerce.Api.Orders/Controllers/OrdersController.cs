using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProvider orderProvider;

        public OrdersController(IOrderProvider orderProvider)
        {
            this.orderProvider = orderProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrderAsync(int customerId)
        { 
            var result = await orderProvider.GetOrderAsync(customerId);
            if (result.isSuccess)
            {
                return Ok(result.orders);
            }
            return BadRequest();
        }
    }
}
