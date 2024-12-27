using BayanKuaforOtomasyonu.Models.ViewModels.AppUserEmploymentViewModel;
using BayanKuaforOtomasyonu.Models.ViewModels.EmploymentViewModel;

namespace BayanKuaforOtomasyonu.Services.Abstracts
{
    public interface ITrackingService
    {
        List<EmployeeTrackingViewModel> GetAllEmployeesTracking();
        List<EmploymentTrackViewModel> GetAllEmploymentsTracking();
    }
}
