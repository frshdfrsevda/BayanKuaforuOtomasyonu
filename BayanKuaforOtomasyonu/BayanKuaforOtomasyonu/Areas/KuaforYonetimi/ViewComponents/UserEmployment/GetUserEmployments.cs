using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BayanKuaforOtomasyonu.Areas.KuaforYonetimi.ViewComponents.UserEmployment
{
    public class GetUserEmployments : ViewComponent
    {
        private readonly IUserEmploymentService _userEmploymentService;

        public GetUserEmployments(IUserEmploymentService userEmploymentService)
        {
            _userEmploymentService = userEmploymentService;
        }
        public IViewComponentResult Invoke(string userId)
        {
            ViewBag.Id = userId;
            return View(_userEmploymentService.GetAllUserEmployments(userId));
        }
    }
}
