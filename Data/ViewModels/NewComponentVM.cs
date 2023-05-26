using System.ComponentModel.DataAnnotations;
namespace OnlineShop.Data.ViewModels
{
    public class NewComponentVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "URL slike je obavezan")]

        [Display(Name = "URL slike komponente")]
        public string ComponentsPictureUrl { get; set; }

        [Required(ErrorMessage = "Naziv proizvođača je obavezan")]

        [Display(Name = "Proizvođač")]

        public string Manufacturer { get; set; }


        [Required(ErrorMessage = "Garancija je obavezna")]

        [Display(Name = "Garancija")]
        public string Warranty { get; set; }

        [Required(ErrorMessage = "Naziv modela je obavezan")]

        [Display(Name = "Model")]
        public string Model { get; set; }

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
        public int ComponentsTypesId { get; set; }
    }
}
