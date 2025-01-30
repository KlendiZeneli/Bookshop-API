using UserLogin.Models;
using UserLogin.Dtos;
using UserLogin.Data;

namespace UserLogin.Data
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<Orders> CreateOrderAsync( OrderDetailsDto orderDetails)
        //{
        //    // Validate shipping method and related fields
        //    if (orderDetails.ShippingMethod == "pickup" && string.IsNullOrEmpty(orderDetails.Store))
        //    {
        //        throw new ArgumentException("Store is required for pickup orders.");
        //    }

        //    if (orderDetails.ShippingMethod == "delivery" && (string.IsNullOrEmpty(orderDetails.City) || string.IsNullOrEmpty(orderDetails.Address)))
        //    {
        //        throw new ArgumentException("City and address are required for delivery orders.");
        //    }

        //    // Calculate the total price of the order
        //    decimal total = cart.Items.Sum(item => item.Book.Price * item.Quantity);

        //    var order = new Orders
        //    {
        //        Name = orderDetails.Name,
        //        Phone = orderDetails.Phone,
        //        Email = orderDetails.Email,
        //        ShippingMethod = orderDetails.ShippingMethod,
        //        Store = orderDetails.ShippingMethod == "pickup" ? orderDetails.Store : null,
        //        City = orderDetails.ShippingMethod == "delivery" ? orderDetails.City : null,
        //        Address = orderDetails.ShippingMethod == "delivery" ? orderDetails.Address : null,
        //        SpecialComments = orderDetails.SpecialComments,
        //        OrderDate = DateTime.UtcNow,
        //        Total = total,
        //        Items = cart.Items.Select(item => new OrderItems
        //        {
        //            ISBN = item.ISBN,
        //            Quantity = item.Quantity,
        //            Price = item.Book.Price // Store the price at the time of purchase
        //        }).ToList(),
        //        IsDelivered = false // Default to not delivered
        //    };

        //    _context.Orders.Add(order);
        //    _context.Carts.Remove(cart); // Empty the cart
        //    await _context.SaveChangesAsync();

        //    return order;
        //}
    }
}
