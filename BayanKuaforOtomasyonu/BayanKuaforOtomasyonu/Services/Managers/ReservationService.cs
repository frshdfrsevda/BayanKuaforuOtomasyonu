using Azure.Core;
using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace BayanKuaforOtomasyonu.Services.Managers
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationDetailRepository _reservationDetailRepository;
        private readonly UserManager<AppUser> _userManager;

        public ReservationService(IReservationRepository reservationRepository, UserManager<AppUser> userManager, IReservationDetailRepository reservationDetailRepository)
        {
            _reservationRepository = reservationRepository;
            _userManager = userManager;
            _reservationDetailRepository = reservationDetailRepository;
        }

        public async Task CreateReservationAsync(AddReservationViewModel viewModel)
        {
            var currentUser = await _userManager.FindByNameAsync(viewModel.AppUserId);
            viewModel.AppUserId = currentUser.Id;
            var res = new Reservation()
            {
                AppUserId = viewModel.AppUserId,
                ResDate = viewModel.ResDate,
                ResTime = viewModel.ResTime,
                TotalDuration = viewModel.TotalDuration,
                TotalPrice = viewModel.TotalPrice
            };
            _reservationRepository.Add(res);
            _reservationRepository.Save();
            var userEmployments = new List<int>();
            foreach (var prop in viewModel.ReservationDetailIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                _reservationDetailRepository.Add(new ReservationDetail()
                {
                    ResId= res.Id,
                    AppUserEmploymentId=int.Parse(prop)
                });
            }
            _reservationDetailRepository.Save();
        }
    }
}
