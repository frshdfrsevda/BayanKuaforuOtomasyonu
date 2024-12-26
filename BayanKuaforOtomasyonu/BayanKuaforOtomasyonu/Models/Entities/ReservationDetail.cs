using System.ComponentModel.DataAnnotations.Schema;

namespace BayanKuaforOtomasyonu.Models.Entities
{
    public class ReservationDetail
    {
        public int Id { get; set; }
        public int ResId { get; set; }
        [ForeignKey(nameof(ResId))]
        public Reservation Reservation { get; set; }
        public int AppUserEmploymentId { get; set; }
        [ForeignKey(nameof(AppUserEmploymentId))]
        public AppUserEmployement AppUserEmployement { get; set; }
    }
}
