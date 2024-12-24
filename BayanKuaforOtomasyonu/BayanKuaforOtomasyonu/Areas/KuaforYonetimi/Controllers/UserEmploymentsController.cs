using BayanKuaforOtomasyonu.Models.ViewModels.AppUserEmploymentViewModel;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace BayanKuaforOtomasyonu.Areas.KuaforYonetimi.Controllers
{
    [Area("KuaforYonetimi")]
    public class UserEmploymentsController : Controller
    {
        private readonly IUserEmploymentService _userEmploymentService;
        private readonly IEmploymentService _employmentService;
        public UserEmploymentsController(IUserEmploymentService userEmploymentService, IEmploymentService employmentService)
        {
            _userEmploymentService = userEmploymentService;
            _employmentService = employmentService;
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
    }
}
