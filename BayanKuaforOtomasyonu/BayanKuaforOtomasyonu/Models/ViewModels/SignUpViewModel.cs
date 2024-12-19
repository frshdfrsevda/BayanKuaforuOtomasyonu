using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.ViewModels
{
    public class SignUpViewModel
    {
        [EmailAddress(ErrorMessage = "Email adresi alanı boş bırakılamaz!")]
        [Display(Name = "Email Adresi*")]
        public string EmailAddress { get; set; }
        [Display(Name = "Ad Soyad*")]
        [Required(ErrorMessage = "Ad Soyad alanı boş bırakılamaz!")]
        public string NameSurname { get; set; }
        [Phone(ErrorMessage = "Telefon numarası alanı boş bırakılamaz!")]
        [Display(Name = "Telefon Numarası*")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Şifre*")]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz!")]
        public string Password { get; set; }
        [Display(Name = "Şifre Tekrar*")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre Tekrar alanı boş bırakılamaz!")]
        [Compare(nameof(Password),ErrorMessage ="Şifreler uyuşmadı!")]
        public string ConfirmPassword { get; set; }
    }
}
