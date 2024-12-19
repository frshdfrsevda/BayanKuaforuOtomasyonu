using Microsoft.AspNetCore.Identity;

namespace BayanKuaforOtomasyonu.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public string NameSurname { get; set; }
    }
}
