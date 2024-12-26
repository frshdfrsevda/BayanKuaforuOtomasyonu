using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace BayanKuaforOtomasyonu.Services.Managers
{
    public class TrackingService : ITrackingService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IReservationStatusRepository _reservationStatusRepository;

        public TrackingService(UserManager<AppUser> userManager, IReservationStatusRepository reservationStatusRepository)
        {
            _userManager = userManager;
            _reservationStatusRepository = reservationStatusRepository;
        }

        public void GetAllEmployeesTracking()
        {
            throw new NotImplementedException();
        }
    }
}
