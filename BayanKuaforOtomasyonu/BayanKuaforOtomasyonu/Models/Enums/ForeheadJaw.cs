using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.Enums
{
    public enum ForeheadJaw
    {
        [Display(Name = "Geniş Alın")]
        WideForehead,

        [Display(Name = "Dar Alın")]
        NarrowForehead,

        [Display(Name = "Belirgin Çene")]
        ProminentChin
    }
}
