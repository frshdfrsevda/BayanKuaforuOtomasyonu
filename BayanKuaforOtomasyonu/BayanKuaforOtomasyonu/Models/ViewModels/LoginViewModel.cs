using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage ="Email format is wrong!")]
        [Display(Name = "Email Address*")]
        public string EmailAddress { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password field is required!")]
        [Display(Name ="Password*")]
        public string Password { get; set; }
    }
}
