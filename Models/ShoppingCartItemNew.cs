using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class ShoppingCartItemNew
    {
        [Key]
        public int Id { get; set; }

        public Laptop? Laptop { get; set; }
        public Components? Component { get; set; }
        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
