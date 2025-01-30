namespace UserLogin.Dtos
{
    public class CartItemDto
    {
        public string ISBN { get; set; }
        public int Quantity { get; set; }
        public int? UserId { get; set; }
        public string SessionId { get; set; }
    }
}
