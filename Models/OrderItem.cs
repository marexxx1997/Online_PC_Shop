using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }

        public int LaptopId { get; set; }
        [ForeignKey("LaptopId")]

        public virtual Laptop Laptop { get; set; }

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]

        public virtual Order Order { get; set; }
    }
}
