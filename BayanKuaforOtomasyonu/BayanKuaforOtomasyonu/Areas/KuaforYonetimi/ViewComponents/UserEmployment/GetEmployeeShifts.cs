using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BayanKuaforOtomasyonu.Areas.KuaforYonetimi.ViewComponents.UserEmployment
{
    public class GetEmployeeShifts : ViewComponent
    {
        private readonly IEmployeeShiftService _employeeShiftService;

        public GetEmployeeShifts(IEmployeeShiftService employeeShiftService)
        {
            _employeeShiftService = employeeShiftService;
        }
        public IViewComponentResult Invoke(string userid)
        {
            var models = _employeeShiftService.GetAllEmployeeShiftsById(userid);
            return View(models);
        }
    }
}
