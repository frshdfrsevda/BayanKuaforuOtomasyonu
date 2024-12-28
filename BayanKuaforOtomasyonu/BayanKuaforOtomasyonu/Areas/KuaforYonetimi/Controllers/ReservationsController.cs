using BayanKuaforOtomasyonu.Models;
using BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BayanKuaforOtomasyonu.Areas.KuaforYonetimi.Controllers
{
    [Area("KuaforYonetimi")]
    [Authorize(Roles = "Admin")]
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public IActionResult Index(string? status)
        {
            if (status == "Onaylananlar")
            {
                return View(_reservationService.GetAllByStatus(true));
            }
            if(status == "Reddedilenler")
            {
                return View(_reservationService.GetAllByStatus(false));
            }
            return View(_reservationService.GetAllByStatus(null));
        }
        [HttpPost]
        public IActionResult UpdateReservationDate([FromBody] GeciciModel geciciModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Geçersiz veri gönderildi." });
            }
            var model = new ChangeDateResViewModel()
            {
                Id = int.Parse(geciciModel.Id),
                ResDate = DateTime.Parse(geciciModel.ResDate),
                ResTime = TimeSpan.Parse(geciciModel.ResTime)
            };
            var (isok,msg) = _reservationService.ChangeReservationDate(model);
            if(isok) 
                _reservationService.ChangeStatusForManager(model.Id);
            return Json(new { success = true, message = msg });
        }
        public IActionResult Decline(int resid)
        {
            _reservationService.DeclineRes(resid);
            return RedirectToAction("Index", "Reservations", new {area="KuaforYonetimi"});
        }
        public IActionResult Accept(int resid)
        {
            _reservationService.ApproveRes(resid);
            return RedirectToAction("Index", "Reservations", new { area = "KuaforYonetimi" });
        }
    }
}
