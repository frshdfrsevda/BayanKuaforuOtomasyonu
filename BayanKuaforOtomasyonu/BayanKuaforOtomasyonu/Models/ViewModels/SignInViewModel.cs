using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.ViewModels
{
    public class SignInViewModel
    {
        [EmailAddress(ErrorMessage = "Email format is wrong!")]
        [Display(Name = "Email Address*")]
        public string EmailAddress { get; set; }
        [Display(Name = "Name Surname*")]
        [Required(ErrorMessage = "Name Surname field is required!")]
        public string NameSurname { get; set; }
        [Phone(ErrorMessage ="Phone number format is wrong!")]
        [Display(Name = "Phone Number*")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password*")]
        [Required(ErrorMessage = "Password field is required!")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password*")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password field is required!")]
        [Compare(nameof(Password),ErrorMessage ="Password did not match!")]
        public string ConfirmPassword { get; set; }
    }
}
