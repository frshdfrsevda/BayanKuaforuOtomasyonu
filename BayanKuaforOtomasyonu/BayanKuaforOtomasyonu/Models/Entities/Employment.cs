using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.Entities
{
    public class Employment
    {
        [Key]
        public int Id { get; set; }
        [Display(Name="Hizmet")]
        [Required(ErrorMessage ="Hizmet alanı zorunludur!")]
        public string Name { get; set; }
        public ICollection<AppUserEmployement> AppUserEmployements { get; set; }
    }
}
