using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class OrderItemNew
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }

        public int? LaptopId { get; set; }
        [ForeignKey("LaptopId")]

        public virtual Laptop? Laptop { get; set; }/*= new Laptop();*/

        public int? ComponentId { get; set; }
        [ForeignKey("ComponentId")]

        public virtual Components? Component { get; set; } /*= new Components();*/


        public int OrderId { get; set; }
        [ForeignKey("OrderId")]

        public virtual Order Order { get; set; }
    }
}
