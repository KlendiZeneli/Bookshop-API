namespace UserLogin.Dtos
{
    public class OrderDetailsDto
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ShippingMethod { get; set; } // e.g., "delivery" or "pickup"
        public string? Store { get; set; } // Required if ShippingMethod is "pickup"
        public string? City { get; set; } // Required if ShippingMethod is "delivery"
        public string? Address { get; set; } // Required if ShippingMethod is "delivery"
        public string? SpecialComments { get; set; }
        public List<OrderItemDto> Items { get; set; } // List of order items
    }
}
