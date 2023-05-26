using OnlineShop.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Components:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Warranty { get; set; }

        public string Description { get; set; }

        public int ComponentsTypesId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string ComponentsPictureUrl { get; set; }
        
    }
}
