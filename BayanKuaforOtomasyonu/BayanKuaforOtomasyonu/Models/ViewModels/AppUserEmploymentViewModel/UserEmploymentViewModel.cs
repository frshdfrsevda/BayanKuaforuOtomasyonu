using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.ViewModels.AppUserEmploymentViewModel
{
    public class UserEmploymentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Hizmet seçilmelidir!")]
        [Display(Name ="Hizmetler")]
        public int EmploymentId { get; set; }
        [Required(ErrorMessage = "Kullanıcı seçilmelidir!")]
        [Display(Name = "Kullanıcı")]
        public string AppUserId { get; set; }
        [Required(ErrorMessage = "Fiyat zorunludur!")]
        [Display(Name = "Fiyat")]
        public int Price { get; set; }
        [Required(ErrorMessage = "İşlem süresi zorunludur!")]
        [Display(Name = "Süre")]
        public int Duration { get; set; }
    }
}
