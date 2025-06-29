using MusicShop.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace MusicShop.Models
{
    public class RegisterModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        [Length(4, 16, ErrorMessage = "Username must be between 4 and 16 characters long")]
        [ContainsLatinCharsAndDigits(ErrorMessage = "Username must contain only latin letters and digits")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [ContainsAtLeastOneLetterAndDigit(ErrorMessage = "Password must contain at least one letter and one digit")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email is required")]
        [IsValidEmailAddress(ErrorMessage = "Invalid Email address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}
