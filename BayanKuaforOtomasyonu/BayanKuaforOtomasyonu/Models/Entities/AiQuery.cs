using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BayanKuaforOtomasyonu.Models.Entities
{
    public class AiQuery
    {
        [Key]
        public int Id { get; set; }
        public string Query { get; set; }
        public string Response { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string AppUserId { get; set; }
        [ForeignKey(nameof(AppUserId))]
        public AppUser AppUser { get; set; }
    }
}
