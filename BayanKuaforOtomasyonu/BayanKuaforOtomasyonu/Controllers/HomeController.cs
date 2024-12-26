using System.Diagnostics;
using BayanKuaforOtomasyonu.Models;
using BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BayanKuaforOtomasyonu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmploymentService _employmentService;
        private readonly IUserEmploymentService _userEmploymentService;
        private readonly IReservationService _reservationService;
        public HomeController(ILogger<HomeController> logger, IEmploymentService employmentService, IUserEmploymentService userEmploymentService, IReservationService reservationService)
        {
            _logger = logger;
            _employmentService = employmentService;
            _userEmploymentService = userEmploymentService;
            _reservationService = reservationService;
        }

        public IActionResult Index()
        {
            var listModel = _employmentService.GetAllEmploymentsWithIncludes();
            
            return View(new HomeIndexModel() { ListModel = listModel });
        }
        [HttpGet]
        public IActionResult GetUserEmploymentData(int userId)
        {
            return Json(_userEmploymentService.GetUserEmploymentById(userId));
        }
        [HttpPost]
        public async Task<IActionResult> CreateReservation(AddReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent("PostReservation", model);
            }
            // Ek doðrulama: ResDate geçmiþ bir tarih mi?
            if (model.ResDate <= DateTime.Now)
            {
                ModelState.AddModelError(nameof(model.ResDate), "Geçmiþ bir tarih seçemezsiniz.");
                return ViewComponent("PostReservation", model);
            }
            model.AppUserId = User.Identity.Name;
            await _reservationService.CreateReservationAsync(model);

            return Json(new { success = true, message = "ok" });
        }
        [HttpGet]
        public IActionResult Reservations()
        {
            return View(_reservationService.GetAllByUser(User.Identity.Name));
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
            var (isok, msg) = _reservationService.ChangeReservationDate(model);
            if (isok)
                _reservationService.ChangeStatusForUser(model.Id);
            return Json(new { success = true, message = msg });
        }
        public IActionResult Decline(int resid)
        {
            _reservationService.DeclineRes(resid);
            return RedirectToAction("Reservations", "Home", new { area = "" });
        }
        public IActionResult Accept(int resid)
        {
            _reservationService.ChangeStatusForUser(resid);
            return RedirectToAction("Reservations", "Home", new { area = "" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
