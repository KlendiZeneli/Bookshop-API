namespace UserLogin.Models
{
    public class OrderItems
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Foreign Key
        public Orders Order { get; set; } // Navigation property
        public string ISBN { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Price at the time of purchase
    }
}
