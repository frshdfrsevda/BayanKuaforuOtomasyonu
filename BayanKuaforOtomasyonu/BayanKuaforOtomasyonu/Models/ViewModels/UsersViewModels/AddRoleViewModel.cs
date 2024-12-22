using System.ComponentModel.DataAnnotations;

namespace BayanKuaforOtomasyonu.Models.ViewModels.UsersViewModels
{
    public class AddRoleViewModel
    {
        [Required(ErrorMessage ="Kullanıcı bilgisi alınamadı!")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Rol bilgisi alınamadı!")]
        public string RoleName { get; set; }
    }
}
