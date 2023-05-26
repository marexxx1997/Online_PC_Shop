using OnlineShop.Data.Base;
using OnlineShop.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Laptop:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display (Name ="Profile Picture URL")]
        public string ProfilePictureUrl { get; set; }

        [Display(Name = "Manufacturer")]

        public Manufacturer Manufacturer { get; set; }

        [Display(Name = "Screen Size")]
        public string ScreenSize { get; set; }

        public string Warranty { get; set; }

        public string Model { get; set; }
        public GraphicCard GraphicCard { get; set; }

        public string HardDisc { get; set; }

        public string OS { get; set; }

        public string Processor { get; set; }

        public string RAM { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public List<LaptopBrand> LaptopBrands { get; set; }


        [ForeignKey("LaptopsTypes")]
        public int LaptopsTypesId { get; set; }
        public virtual LaptopTypes LaptopsTypes { get; set; }

    }
}
