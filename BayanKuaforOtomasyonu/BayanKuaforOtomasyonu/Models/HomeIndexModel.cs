using BayanKuaforOtomasyonu.Models.ViewModels.EmploymentViewModel;
using BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels;

namespace BayanKuaforOtomasyonu.Models
{
    public class HomeIndexModel
    {
        public List<EmploymentWithIncludesViewModel> ListModel { get; set; }
        public AddReservationViewModel ResModel { get; set; } = new AddReservationViewModel();
    }
}
