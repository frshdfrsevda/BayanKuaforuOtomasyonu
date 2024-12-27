using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Models.ViewModels.AppUserEmploymentViewModel;
using BayanKuaforOtomasyonu.Models.ViewModels.EmploymentViewModel;
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

        public List<EmployeeTrackingViewModel> GetAllEmployeesTracking()
        {
            var reservations = _reservationStatusRepository.GetAllByCondition(rs => rs.isStatus == true, "Reservation,Reservation.AppUser,Reservation.ReservationDetails,Reservation.ReservationDetails.AppUserEmployement.AppUser,,Reservation.ReservationDetails.AppUserEmployement.Employment");
            
            var listModel = new List<EmployeeEmploymentInfoViewModel>();
            foreach (var reservation in reservations)
            {
                foreach(var info in reservation.Reservation.ReservationDetails)
                {
                    var model = new EmployeeEmploymentInfoViewModel()
                    {
                        UserName = info.AppUserEmployement.AppUser.Email,
                        EmployeeName = info.AppUserEmployement.AppUser.NameSurname,
                        Price = info.AppUserEmployement.Price
                    };
                    listModel.Add(model);
                }
            }

            return listModel.GroupBy(lm=>lm.EmployeeName)
                .Select(g=>new EmployeeTrackingViewModel
                {
                    EmployeeName = g.Key,
                    TotalReservation = g.Count(),
                    TotalMoney = g.Sum(lm=>lm.Price)
                }).OrderByDescending(e=>e.TotalMoney).ToList();
            
        }
        public List<EmploymentTrackViewModel> GetAllEmploymentsTracking()
        {
            var reservations = _reservationStatusRepository.GetAllByCondition(rs => rs.isStatus == true, "Reservation,Reservation.AppUser,Reservation.ReservationDetails,Reservation.ReservationDetails.AppUserEmployement.AppUser,,Reservation.ReservationDetails.AppUserEmployement.Employment");

            var listModel = new List<EmploymentTrackViewModel>();
            foreach (var reservation in reservations)
            {
                foreach (var info in reservation.Reservation.ReservationDetails)
                {
                    var model = new EmploymentTrackViewModel()
                    {
                        Name = info.AppUserEmployement.Employment.Name
                    };
                    listModel.Add(model);
                }
            }

            return listModel.GroupBy(lm => lm.Name)
                .Select(g => new EmploymentTrackViewModel
                {
                    Name = g.Key,
                    Total = g.Count()
                }).OrderByDescending(e => e.Total).ToList();

        }
    }
}
