using Microsoft.AspNetCore.Identity;

namespace BayanKuaforOtomasyonu.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string NameSurname { get; set; }
        public ICollection<AppUserEmployement> AppUserEmployements { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<AiQuery> AiQueries { get; set; }
        public ICollection<EmployeeShift> EmployeeShifts { get; set; }
    }
}
