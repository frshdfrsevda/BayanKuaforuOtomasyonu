using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.ViewModels.AiQuery
{
    public class AddAiQueryViewModel
    {
        [Required(ErrorMessage ="Sorgunuz Alınamadı")]
        public string Query { get; set; }
        [Required(ErrorMessage = "Cevap Alınamadı")]
        public string Response { get; set; }
        [Required(ErrorMessage = "Kullanıcı Bilgisi Alınamadı")]
        public string AppUserId { get; set; }
    }
}
