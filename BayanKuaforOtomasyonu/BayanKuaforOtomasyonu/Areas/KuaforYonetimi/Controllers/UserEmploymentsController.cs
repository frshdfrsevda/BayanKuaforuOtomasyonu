using BayanKuaforOtomasyonu.Models.ViewModels.AppUserEmploymentViewModel;
using BayanKuaforOtomasyonu.Models.ViewModels.EmployeeShiftViewModels;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace BayanKuaforOtomasyonu.Areas.KuaforYonetimi.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("KuaforYonetimi")]
    public class UserEmploymentsController : Controller
    {
        private readonly IUserEmploymentService _userEmploymentService;
        private readonly IEmploymentService _employmentService;
        private readonly ITrackingService _trackingService;
        private readonly IEmployeeShiftService _employeeShiftService;
        public UserEmploymentsController(IUserEmploymentService userEmploymentService, IEmploymentService employmentService, ITrackingService trackingService, IEmployeeShiftService employeeShiftService)
        {
            _userEmploymentService = userEmploymentService;
            _employmentService = employmentService;
            _trackingService = trackingService;
            _employeeShiftService = employeeShiftService;
        }
        public IActionResult LoadUserEmploymentsTable(string userid)
        {
            return ViewComponent("GetUserEmployments", new { userId = userid });
        }
        [HttpGet]
        public IActionResult Index(string id)
        {
            var employments = _employmentService.GetAllEmployments();
            var selectlist = new SelectList(employments, "Id", "Name");
            ViewBag.UserId = id;
            ViewBag.SelectList = selectlist;
            return View(new UserEmploymentViewModel());
        }
        [HttpPost]
        public IActionResult Index(UserEmploymentViewModel model)
        {
            if (model.Id == 0)
            {
                ModelState.Remove("Id");
            }
            if (!ModelState.IsValid) {
                return View(model);
            }
            _userEmploymentService.CreateOrUpdateUserEmployment(model);
            return RedirectToAction("Index", "UserEmployments", new {area="KuaforYonetimi", id=model.AppUserId});
        }
        public IActionResult Delete(int id, string userId) {
            _userEmploymentService.DeleteUserEmployment(id);
            return RedirectToAction("Index", "UserEmployments", new { area = "KuaforYonetimi", id = userId });
        }
        public IActionResult Tracking()
        {
            return View(_trackingService.GetAllEmployeesTracking());
        }
        [HttpGet]
        public IActionResult EmployeeShift(string userid)
        {
            var models = _employeeShiftService.GetAllEmployeeShiftsById(userid);
            List<SelectListItem> days = new List<SelectListItem>
            {
                new SelectListItem { Text = "Pazartesi", Value = "1" },
                new SelectListItem { Text = "Salı", Value = "2" },
                new SelectListItem { Text = "Çarşamba", Value = "3" },
                new SelectListItem { Text = "Perşembe", Value = "4" },
                new SelectListItem { Text = "Cuma", Value = "5" },
                new SelectListItem { Text = "Cumartesi", Value = "6" },
                new SelectListItem { Text = "Pazar", Value = "0" },
            };
            foreach (var model in models)
            {
                var intDay = ((int)model.ShiftDay).ToString();
                days = days.Where(d=>d.Value != intDay).ToList();
            }
            ViewBag.Days = days;
            return View(new AddEmployeeShiftViewModel() { AppUserId=userid});
        }
        [HttpPost]
        public IActionResult EmployeeShift(AddEmployeeShiftViewModel addModel)
        {
            var models = _employeeShiftService.GetAllEmployeeShiftsById(addModel.AppUserId);
            if (!ModelState.IsValid)
            {
                TempData["Shift"] = "Lütfen bütün alanları doldurduğunuzdan emin olun";
                return RedirectToAction("EmployeeShift", "UserEmployments", new { area = "KuaforYonetimi", userid = addModel.AppUserId });
            }
            if(models.Where(m=>m.ShiftDay == addModel.ShiftDay).ToList().Count() > 0)
            {
                TempData["Shift"] = "İlgili gün için mesai bulunuyor, lütfen güncelleme işlemi yapınız";
                return RedirectToAction("EmployeeShift", "UserEmployments", new { area = "KuaforYonetimi", userid = addModel.AppUserId });
            }
            _employeeShiftService.CreateEmployeeShift(addModel);
            TempData["Shift"] = "Ekleme işlemi başarıyla yapıldı";
            return RedirectToAction("EmployeeShift", "UserEmployments", new { area = "KuaforYonetimi", userid = addModel.AppUserId });
        }
        [HttpPost]
        public IActionResult UpdateShift([FromBody] ShiftUpdateModel model)
        {
            if (model == null)
            {
                return BadRequest("Geçersiz veri");
            }
            var updateModel = new EmployeeShiftViewModel()
            {
                Id = model.Id,
                FirstTİme = TimeSpan.Parse(model.FirstTime),
                LastTime = TimeSpan.Parse(model.LastTime)
            };
            _employeeShiftService.UpdateEmployeeShift(updateModel);
            return Ok("Güncelleme başarılı");
        }
        public IActionResult DeleteShift(int id)
        {
            var model = _employeeShiftService.GetEmployeeShiftById(id);
            _employeeShiftService.DeleteEmployeeShift(id);
            return RedirectToAction("EmployeeShift", "UserEmployments", new { area = "KuaforYonetimi", userid = model.AppUserId });
        }
    }
}
