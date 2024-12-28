using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.Enums
{
    public enum Lifestyle
    {
        [Display(Name = "Günlük")]
        Casual,

        [Display(Name = "Meslek")]
        Professional,

        [Display(Name = "Modern")]
        Modern
    }
}
