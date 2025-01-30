using UserLogin.Data;
using UserLogin.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("complete")]
        public async Task<IActionResult> CompleteOrder([FromBody] OrderDetailsDto orderDetails)
        {
            if (orderDetails == null || orderDetails.Items == null || !orderDetails.Items.Any())
            {
                return BadRequest("Order must contain at least one item.");
            }

            try
            {
                var order = await _orderService.CreateOrderAsync(orderDetails);
                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
