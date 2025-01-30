using Microsoft.EntityFrameworkCore;
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

        public async Task<Orders> CreateOrderAsync(OrderDetailsDto orderDetails)
        {
            // Validate shipping method and required fields
            if (orderDetails.ShippingMethod == "pickup" && string.IsNullOrEmpty(orderDetails.Store))
            {
                throw new ArgumentException("Store is required for pickup orders.");
            }

            if (orderDetails.ShippingMethod == "delivery" && (string.IsNullOrEmpty(orderDetails.City) || string.IsNullOrEmpty(orderDetails.Address)))
            {
                throw new ArgumentException("City and address are required for delivery orders.");
            }

            if (orderDetails.Items == null || !orderDetails.Items.Any())
            {
                throw new ArgumentException("Order must contain at least one item.");
            }

            // Fetch books from the database based on ISBNs in the order
            var bookIsbns = orderDetails.Items.Select(i => i.ISBN).ToList();
            var books = await _context.Books
                                      .Where(b => bookIsbns.Contains(b.ISBN))
                                      .ToListAsync();

            if (books.Count != bookIsbns.Count)
            {
                throw new ArgumentException("Some books in the order do not exist in the database.");
            }

            // Validate stock and calculate total price
            decimal total = 0;
            List<OrderItems> orderItems = new List<OrderItems>();

            foreach (var item in orderDetails.Items)
            {
                var book = books.FirstOrDefault(b => b.ISBN == item.ISBN);
                if (book == null)
                {
                    throw new ArgumentException($"Book with ISBN {item.ISBN} does not exist.");
                }

                if (book.quantityInStock < item.Quantity)
                {
                    throw new ArgumentException($"Not enough stock for '{book.Title}'. Available: {book.quantityInStock}, Requested: {item.Quantity}");
                }

                // Reduce stock in the database
                book.quantityInStock -= item.Quantity;

                // Use database price instead of the frontend-submitted price
                decimal itemPrice = book.Price;
                total += itemPrice * item.Quantity;

                orderItems.Add(new OrderItems
                {
                    ISBN = item.ISBN,
                    Book = book,
                    Quantity = item.Quantity,
                    Price = itemPrice // Use correct price from DB
                });
            }

            var order = new Orders
            {
                Name = orderDetails.Name,
                Phone = orderDetails.Phone,
                Email = orderDetails.Email,
                ShippingMethod = orderDetails.ShippingMethod,
                Store = orderDetails.ShippingMethod == "pickup" ? orderDetails.Store : null,
                City = orderDetails.ShippingMethod == "delivery" ? orderDetails.City : null,
                Address = orderDetails.ShippingMethod == "delivery" ? orderDetails.Address : null,
                SpecialComments = orderDetails.SpecialComments,
                OrderDate = DateTime.UtcNow,
                Total = total,
                Items = orderItems,
                IsDelivered = false
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
