using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BayanKuaforOtomasyonu.Areas.KuaforYonetimi.ViewComponents.UserEmployment
{
    public class GetEmploymentsTracking : ViewComponent
    {
        private readonly ITrackingService _trackingService;

        public GetEmploymentsTracking(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }
        public IViewComponentResult Invoke()
        {
            return View(_trackingService.GetAllEmploymentsTracking());
        }
    }
}
