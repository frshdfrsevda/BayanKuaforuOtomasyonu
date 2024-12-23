using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.ViewModels.EmploymentViewModel
{
    public class UpdateEmploymentViewMedel
    {
        public int Id { get; set; }
        [Display(Name = "Hizmet")]
        [Required(ErrorMessage = "Hizmet alanı zorunludur!")]
        public string Name { get; set; }
    }
}
