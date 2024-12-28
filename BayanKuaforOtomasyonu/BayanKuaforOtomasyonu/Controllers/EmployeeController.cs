
using BayanKuaforOtomasyonu.Data.Repos.Abstracts;
using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BayanKuaforOtomasyonu.Controllers
{
    [Authorize(Roles ="Official")]
    public class EmployeeController : Controller
    {
        private readonly IUserEmploymentRepository _userEmploymentService;
        private readonly IReservationService _reservationService;
        private readonly UserManager<AppUser> _userManager;
        private string userName => User.Identity!.Name!;

        public EmployeeController(IUserEmploymentRepository userEmploymentService, UserManager<AppUser> userManager, IReservationService reservationService)
        {
            _userEmploymentService = userEmploymentService;
            _userManager = userManager;
            _reservationService = reservationService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = _userEmploymentService.GetAllByCondition(r => r.AppUserId ==user.Id, "Employment,ReservationDetails,ReservationDetails.Reservation,ReservationDetails.Reservation.AppUser");
            var listModel = new List<EmployeeReservationModel>();
            foreach (var item in result)
            {
                var duration = item.Duration;
                var price  = item.Price;
                var employmentName = item.Employment.Name;
                foreach(var itemDetails in item.ReservationDetails)
                {
                    if (_reservationService.CheckResStatus(itemDetails.ResId) == true)
                    {
                        var model = new EmployeeReservationModel()
                        {
                            EmploymentName = employmentName,
                            EmploymentDuration = duration,
                            EmploymentPrice = price,
                            ReservationDate = itemDetails.Reservation.ResDate,
                            ReservationStart = itemDetails.Reservation.ResTime,
                            ReservationEnd = itemDetails.Reservation.ResTime.Add(TimeSpan.FromMinutes(itemDetails.Reservation.TotalDuration)),
                            ReservationTotalPrice = itemDetails.Reservation.TotalPrice,
                            ReservationTotalCustomerName = itemDetails.Reservation.AppUser.NameSurname
                        };
                        listModel.Add(model);
                    }
                }
            }
            return View(listModel.OrderByDescending(x=>x.ReservationDate).ToList());
        }
    }
}
