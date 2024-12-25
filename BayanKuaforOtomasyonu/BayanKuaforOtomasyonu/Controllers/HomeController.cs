using System.Diagnostics;
using BayanKuaforOtomasyonu.Models;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BayanKuaforOtomasyonu.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmploymentService _employmentService;
        private readonly IUserEmploymentService _userEmploymentService;

        public HomeController(ILogger<HomeController> logger, IEmploymentService employmentService, IUserEmploymentService userEmploymentService)
        {
            _logger = logger;
            _employmentService = employmentService;
            _userEmploymentService = userEmploymentService;
        }

        public IActionResult Index()
        {
            return View(_employmentService.GetAllEmploymentsWithIncludes());
        }
        [HttpGet]
        public IActionResult GetUserEmploymentData(int userId)
        {
            return Json(_userEmploymentService.GetUserEmploymentById(userId));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
