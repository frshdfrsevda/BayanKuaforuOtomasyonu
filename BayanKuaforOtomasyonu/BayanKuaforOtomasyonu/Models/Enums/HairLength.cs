using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.Enums
{
    public enum HairLength
    {
        [Display(Name = "Kısa")]
        Short,

        [Display(Name = "Orta")]
        Medium,

        [Display(Name = "Uzun")]
        Long
    }
}
