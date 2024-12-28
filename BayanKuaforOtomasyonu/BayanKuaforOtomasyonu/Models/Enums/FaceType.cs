using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.Enums
{
    public enum FaceType
    {
        [Display(Name = "Oval")]
        Oval,

        [Display(Name = "Yuvarlak")]
        Round,

        [Display(Name = "Kare")]
        Square,

        [Display(Name = "Uzun")]
        Long,

        [Display(Name = "Kalp Şeklinde")]
        HeartShaped,

        [Display(Name = "Elmas")]
        Diamond
    }
}
