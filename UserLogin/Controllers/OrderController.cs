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

        //[HttpPost("complete")]
        //public async Task<IActionResult> CompleteOrder([FromBody] OrderDetailsDto orderDetails)
        //{
        //    var userId = User.FindFirst("sub")?.Value; // Get userId from JWT token
        //    var sessionId = HttpContext.Session.Id; // Get sessionId for guest users

        //    var cart = await _cartService.GetCartAsync(userId != null ? int.Parse(userId) : (int?)null, sessionId);

        //    if (cart == null || !cart.Items.Any())
        //    {
        //        return BadRequest("Cart is empty.");
        //    }

        //    try
        //    {
        //        var order = await _orderService.CreateOrderAsync(cart, orderDetails);
        //        return Ok(order);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
