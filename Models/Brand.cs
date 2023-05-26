using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<LaptopBrand> LaptopBrands { get; set; }

    }
}
