using Octopus.Client.Validation;
using System.ComponentModel.DataAnnotations;
using Xunit;
using Xunit.Sdk;

namespace OnlineShop.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Ime i prezime")]
        [Required(ErrorMessage = "Ime i prezime je obavezno!")]
        public string FullName { get; set; }

        [Display(Name = "Email adresa")]
        [Required(ErrorMessage ="E-mail adresa je obavezna!")]
        public string EmailAddress { get; set; }

        [Display(Name = "Lozinka")]
        [Required(ErrorMessage = "Lozinka je obavezna!")]
        [PasswordComplexity(ErrorMessage = "Lozinka mora sadržati barem jedno veliko slovo, broj i interpunkcijski znak, i imati dužinu od najmanje 8 znakova.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Potvrdite lozinku")]
        [Required(ErrorMessage = "Morate potvrditi lozinku!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Lozinke se ne podudaraju!")]
        public string ConfirmPassword { get; set; }
    }
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string password = value as string;
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (password.Length < 8)
            {
                return false;
            }

            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasDigit = false;
            bool hasPunctuation = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUppercase = true;
                }
                else if (char.IsLower(c))
                {
                    hasLowercase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
                else if (char.IsPunctuation(c))
                {
                    hasPunctuation = true;
                }
            }

            return hasUppercase && hasLowercase && hasDigit && hasPunctuation;
        }
    }
}
