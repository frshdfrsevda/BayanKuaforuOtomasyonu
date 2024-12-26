using BayanKuaforOtomasyonu.Models.ViewModels.ReservationViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BayanKuaforOtomasyonu.ViewComponents.Home
{
    public class PostReservation : ViewComponent
    { 
        public IViewComponentResult Invoke(AddReservationViewModel? model)
        {
            if (model == null)
            {
                model.TotalDuration = 0;
                model.TotalPrice = 0;
            }
            return View(model);
        }
    }
}
