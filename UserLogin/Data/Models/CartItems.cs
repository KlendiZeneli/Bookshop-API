using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserLogin.Models
{
    public class CartItems
    {
        [Key]
        public int Id { get; set; }

        public string ISBN { get; set; }

        public Book Book { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }

        public Cart Cart { get; set; }

        public CartItems() { }

        public CartItems(int cartId, Cart cart)
        {
            CartId = cartId;
            Cart = cart;
        }
    }


}
