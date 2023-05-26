using OnlineShop.Data.Base;
using OnlineShop.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class LaptopTypes : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Laptop> Laptops { get; set; }

    }
}
