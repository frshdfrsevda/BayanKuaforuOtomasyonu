using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BayanKuaforOtomasyonu.Models.Entities
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        public string AppUserId { get; set; }
        [ForeignKey(nameof(AppUserId))]
        public AppUser AppUser { get; set; }
        public DateTime ResDate { get; set; }
        public TimeSpan ResTime { get; set; }
        public int TotalDuration { get; set; }
        public int TotalPrice { get; set; }
        public ICollection<ReservationDetail> ReservationDetails { get; set; }
    }
}
