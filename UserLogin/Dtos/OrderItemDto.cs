namespace UserLogin.Dtos
{
    public class OrderItemDto
    {
        public string ISBN { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Price at the time of order
    }
}
