namespace UserLogin.Models
{
    public class Orders
    {
        public int Id { get; set; }

        // User Information (for both logged-in and guest users)
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Shipping Information
        public string ShippingMethod { get; set; } // e.g., "delivery" or "pickup"
        public string? Store { get; set; } // Required if ShippingMethod is "pickup"
        public string? City { get; set; } // Required if ShippingMethod is "delivery"
        public string? Address { get; set; } // Required if ShippingMethod is "delivery"
        public string? SpecialComments { get; set; }

        // Order Details
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; } // Total price of the order
        public ICollection<OrderItems> Items { get; set; } = new List<OrderItems>();

        // Order Status
        public bool IsDelivered { get; set; } = false; // Default to false (not delivered)
    }
}
