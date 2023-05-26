namespace OnlineShop.Models
{
    public class LaptopBrand
    {
        public int LaptopId { get; set; }

        public Laptop Laptop { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
