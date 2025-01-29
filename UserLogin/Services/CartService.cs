

using Microsoft.EntityFrameworkCore;
using UserLogin.Data;
using UserLogin.Dtos;
using UserLogin.Models;

namespace UserLogin.Data
{
    public class CartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartAsync(int? userId, string? sessionId)
        {
            return await _context.Carts
                .Include(c => c.Items) 
                .ThenInclude(i => i.Book)
                .FirstOrDefaultAsync(c => c.UserId == userId || c.SessionId == sessionId);
        }

        public async Task<Cart> AddToCartAsync(CartItemDto cartItemDto)
        {
            var cart = await GetCartAsync(cartItemDto.UserId, cartItemDto.SessionId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = cartItemDto.UserId,
                    SessionId = cartItemDto.SessionId,
                    Items = new List<CartItems>() 
                };
                _context.Carts.Add(cart);
            }

            // ✅ Ensure cart.CartItems is not null before accessing it
            if (cart.Items == null)
            {
                cart.Items = new List<CartItems>();
            }

            var existingItem = cart.Items.FirstOrDefault(i => i.ISBN == cartItemDto.ISBN);
            if (existingItem != null)
            {
                existingItem.Quantity += cartItemDto.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItems(cart.Id, cart)
                {
                    ISBN = cartItemDto.ISBN,
                    Quantity = cartItemDto.Quantity,
                    CartId = cart.Id, 
                    Cart = cart 
                });
            }

            await _context.SaveChangesAsync();
            return cart;
        }
    }

}

