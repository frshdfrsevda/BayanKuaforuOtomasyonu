using BayanKuaforOtomasyonu.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models
{
    public class ProfileModel
    {
        [Required(ErrorMessage = "Yüz Yapınızı seçmelisiniz")]
        [Display(Name ="Yüz Yapınız")]
        public FaceType Face { get; set; }
        [Required(ErrorMessage = "Saç Tipinizi seçmelisiniz")]
        [Display(Name = "Saç Tipiniz")]
        public HairType Hair { get; set; }
        [Required(ErrorMessage = "Saç Uzunluğu seçmelisiniz")]
        [Display(Name = "Saç Uzunluğu")]
        public HairLength HairLength { get; set; }
        [Required(ErrorMessage = "Alın - Çene Yapınızı seçmelisiniz")]
        [Display(Name = "Alın - Çene Yapınız")]
        public ForeheadJaw ForeheadJaw { get; set; }
        [Required(ErrorMessage = "Yaşam Tarzınızı seçmelisiniz")]
        [Display(Name = "Yaşam Tarzınız")]
        public Lifestyle Lifestyle { get; set; }
        public string? Answer { get; set; }
    }
}
