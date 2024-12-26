using BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels;

namespace BayanKuaforOtomasyonu.Services.Abstracts
{
    public interface IReservationService
    {
        Task CreateReservationAsync(AddReservationViewModel viewModel);
    }
}
