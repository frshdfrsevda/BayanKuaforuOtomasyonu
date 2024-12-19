using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage ="Email adresi alanı boş bırakılamaz!")]
        [Display(Name = "Email Adresi*")]
        public string EmailAddress { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Şifre alanı boş bırakılamaz!")]
        [Display(Name ="Şifre*")]
        public string Password { get; set; }
    }
}
