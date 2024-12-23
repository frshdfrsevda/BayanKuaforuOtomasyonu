using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.ViewModels.EmploymentViewModel
{
    public class AddEmploymentViewModel
    {
        [Display(Name = "Hizmet")]
        [Required(ErrorMessage = "Hizmet alanı zorunludur!")]
        public string Name { get; set; }
    }
}
