using BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels;

namespace BayanKuaforOtomasyonu.Services.Abstracts
{
    public interface IReservationService
    {
        Task CreateReservationAsync(AddReservationViewModel viewModel);
        List<ReservationViewModel> GetAllByStatus(bool? status);
        List<ReservationViewModel> GetAllByUser(string username);
        (bool,string) ChangeReservationDate(ChangeDateResViewModel model);
        bool ControlReservation(DateTime date, TimeSpan time, int totalDuration);
        void ChangeStatusForManager(int resid);
        void ChangeStatusForUser(int resid);
        void DeclineRes(int resid);
        void ApproveRes(int resid);
    }
}
