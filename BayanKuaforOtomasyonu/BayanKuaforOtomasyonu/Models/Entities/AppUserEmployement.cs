using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BayanKuaforOtomasyonu.Models.Entities
{
    public class AppUserEmployement
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Hizmetler")]
        [Required(ErrorMessage ="Hizmet seçilmesi zorunludur!")]
        public int EmploymentId { get; set; }
        [ForeignKey(nameof(EmploymentId))]
        public Employment Employment { get; set; }
        [Display(Name = "Kullanıcılar")]
        [Required(ErrorMessage = "Kullanıcı seçilmesi zorunludur!")]
        public string AppUserId { get; set; }
        [ForeignKey(nameof(AppUserId))]
        public AppUser AppUser { get; set; }
        [Display(Name = "Süre")]
        [Required(ErrorMessage = "Süre alanı zorunludur!")]
        public int Duration { get; set; }
        [Display(Name = "Fiyat")]
        [Required(ErrorMessage = "Fiyat alanı zorunludur!")]
        public int Price { get; set; }
        public ICollection<ReservationDetail> ReservationDetails { get; set; }
    }
}
