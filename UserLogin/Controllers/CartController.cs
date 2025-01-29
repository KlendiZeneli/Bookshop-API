using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserLogin.Data;
using UserLogin.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using UserLogin.Dtos;

namespace UserLogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDto cartItemDto)
        {
            var userId = User.FindFirst("sub")?.Value; // Get userId from JWT token
            var sessionId = HttpContext.Session.Id; // Get sessionId for guest users

            cartItemDto.UserId = userId != null ? int.Parse(userId) : null;
            cartItemDto.SessionId = userId == null ? sessionId : null;

            var updatedCart = await _cartService.AddToCartAsync(cartItemDto);
            return Ok(updatedCart);
        }
    }


}
