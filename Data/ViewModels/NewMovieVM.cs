using OnlineShop.Data.Base;
using OnlineShop.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class NewLaptopVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="URL slike laptopa/računara je obavezan")]

        [Display (Name ="URL slike laptopa/računara")]
        public string ProfilePictureUrl { get; set; }

        [Required(ErrorMessage = "Naziv proizvođača je obavezan")]

        [Display(Name = "Proizvođač")]

        public Manufacturer Manufacturer { get; set; }

        [Required(ErrorMessage = "Veličina ekrana je obavezna")]

        [Display(Name = "Veličina ekrana")]
        public string ScreenSize { get; set; }

        [Required(ErrorMessage = "Garancija je obavezna")]

        [Display(Name = "Garancija")]
        public string Warranty { get; set; }

        [Required(ErrorMessage = "Naziv modela je obavezan")]

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Grafiča kartica je obavezna")]

        [Display(Name = "Grafička kartica")]
        public GraphicCard GraphicCard { get; set; }

        [Required(ErrorMessage = "Hard disk je obavezan")]

        [Display(Name = "Hard disk")]
        public string HardDisc { get; set; }

        [Required(ErrorMessage = "Operativni sistem je obavezan")]

        [Display(Name = "Operativni sistem")]

        public string OS { get; set; }

        [Required(ErrorMessage = "Procesor je obavezan")]

        [Display(Name = "Procesor")]
        public string Processor { get; set; }

        [Required(ErrorMessage = "RAM je obavezan")]

        [Display(Name = "RAM")]
        public string RAM { get; set; }

        [Required(ErrorMessage = "Opis je obavezan")]

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Cijena je obavezna")]

        [Display(Name = "Cijena u KM")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Količina je obavezna")]

        [Display(Name = "Količina")]
        public int Quantity { get; set; }

        //public int BrandId { get; set; }
        [Required(ErrorMessage = "Tip je obavezan")]

        [Display(Name = "Tip proizvoda")]
        public int LaptopsTypesId { get; set; }

    }
}
