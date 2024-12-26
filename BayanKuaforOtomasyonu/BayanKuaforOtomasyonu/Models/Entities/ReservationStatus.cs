using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BayanKuaforOtomasyonu.Models.Entities
{
    public class ReservationStatus
    {
        [Key]
        public int Id { get; set; }
        public int ResId { get; set; }
        [ForeignKey(nameof(ResId))]
        public Reservation Reservation { get; set; }
        public bool? isStatus { get; set; }
        public bool isUserEdit { get; set; } = false;
        public bool isManagerEdit { get; set; } = false;
    }
}
