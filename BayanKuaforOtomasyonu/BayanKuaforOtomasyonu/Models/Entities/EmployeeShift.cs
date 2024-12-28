using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BayanKuaforOtomasyonu.Models.Entities
{
    public class EmployeeShift
    {
        [Key]
        public int Id { get; set; }
        public string AppUserId { get; set; }
        [ForeignKey(nameof(AppUserId))]
        public AppUser AppUser { get; set; }
        [Required]
        public DayOfWeek ShiftDay { get; set; }
        [Required]
        public TimeSpan FirstTİme { get; set; }
        [Required]
        public TimeSpan LastTime { get; set; }
    }
}
