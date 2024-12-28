using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.Enums
{
    public enum HairType
    {
        [Display(Name = "İnce Saçlar")]
        Thin,

        [Display(Name = "Kalın Saçlar")]
        Thick,

        [Display(Name = "Dalgalı Kıvırcık Saçlar")]
        WavyCurly
    }
}
