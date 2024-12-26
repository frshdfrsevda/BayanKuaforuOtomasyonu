using BayanKuaforOtomasyonu.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels
{
    public class AddReservationViewModel
    {
        [Required(ErrorMessage ="Randevunuz için bir tarih bilgisi giriniz")]
        [Display(Name ="Tarih")]
        public DateTime ResDate { get; set; }
        [Required(ErrorMessage = "Randevunuz için bir saat bilgisi giriniz")]
        [Display(Name = "Saat")]
        public TimeSpan ResTime { get; set; }
        [Required(ErrorMessage = "Toplam süre bilgisi alınamadı")]
        [Display(Name = "Toplam Süre (Dakika)")]
        public int TotalDuration { get; set; }
        [Required(ErrorMessage = "Toplam fiyat bilgisi alınamadı")]
        [Display(Name = "Toplam Fiyat (₺)")]
        public int TotalPrice { get; set; }
        public string? AppUserId { get; set; }
        [Required(ErrorMessage = "Hizmetler bilgisi alınamadı")]
        public string ReservationDetailIds { get; set; }
    }
}
