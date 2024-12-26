using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels
{
    public class ChangeDateResViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime ResDate { get; set; }
        [Required]
        public TimeSpan ResTime { get; set; }
    }
}
