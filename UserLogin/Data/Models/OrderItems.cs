using System.Text.Json.Serialization;

namespace UserLogin.Models
{
    public class OrderItems
    {
        public int Id { get; set; }

        public int OrderId { get; set; } // Foreign Key

        [JsonIgnore] // Prevent infinite loop during serialization
        public Orders Order { get; set; } // Navigation property

        public string ISBN { get; set; }
        public virtual Book Book { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // Price at the time of purchase
    }
}
