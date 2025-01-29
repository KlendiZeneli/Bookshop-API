using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UserLogin.Models
{
    public class Cart
    {
        [Key] public int Id { get; set; } // Unique identifier for the cart
        public int? UserId { get; set; } // Nullable for guest users
        public string? SessionId { get; set; } // Used for guest users
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Time when the cart was created

        // Navigation Property
        public required List<CartItems> Items { get; set; } = new();// Items in the cart

        public virtual IdentityUser? User { get; set; }
    }
}
