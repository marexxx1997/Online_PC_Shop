using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Email adresa")]
        [Required(ErrorMessage ="E-mail adresa je obavezna!")]
        public string EmailAddress { get; set; }

        [Display(Name = "Lozinka")]
        [Required(ErrorMessage = "Lozinka je obavezna!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
