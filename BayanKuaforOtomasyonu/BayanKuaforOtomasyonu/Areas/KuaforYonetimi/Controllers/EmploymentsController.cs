using BayanKuaforOtomasyonu.Models.ViewModels.EmploymentViewModel;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BayanKuaforOtomasyonu.Areas.KuaforYonetimi.Controllers
{
    [Area("KuaforYonetimi")]
    public class EmploymentsController : Controller
    {
        private readonly IEmploymentService _employmentService;

        public EmploymentsController(IEmploymentService employmentService)
        {
            _employmentService = employmentService;
        }

        public IActionResult Index()
        {
            return View(_employmentService.GetAllEmployments());
        }
        [HttpPost]
        public IActionResult Add([FromBody]AddEmploymentViewModel viewModel)
        {
            _employmentService.CreateEmployement(viewModel);
            return Json(new { success = true, message = "Kayıt başarılı!" });
        }
        [HttpPost]
        public IActionResult Update([FromBody] UpdateEmploymentViewMedel viewModel)
        {
            _employmentService.UpdateEmployement(viewModel);
            return Json(new { success = true, message = "Kayıt başarılı!" });
        }
        public IActionResult Delete(int id)
        {
            _employmentService.DeleteEmployement(id);
            return RedirectToAction("Index", "Employments", new {area="KuaforYonetimi"});
        }
    }
}
