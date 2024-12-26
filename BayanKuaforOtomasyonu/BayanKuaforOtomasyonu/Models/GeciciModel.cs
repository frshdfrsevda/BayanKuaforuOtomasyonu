using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models
{
    public class GeciciModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string ResDate { get; set; }
        [Required]
        public string ResTime { get; set; }
    }
}
