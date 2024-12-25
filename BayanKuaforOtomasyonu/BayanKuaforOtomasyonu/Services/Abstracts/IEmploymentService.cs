using BayanKuaforOtomasyonu.Models.ViewModels.EmploymentViewModel;

namespace BayanKuaforOtomasyonu.Services.Abstracts
{
    public interface IEmploymentService
    {
        void CreateEmployement(AddEmploymentViewModel addEmploymentViewModel);
        void UpdateEmployement(UpdateEmploymentViewMedel updateEmploymentViewMedel);
        void DeleteEmployement(int id);
        List<UpdateEmploymentViewMedel> GetAllEmployments();
        List<EmploymentWithIncludesViewModel> GetAllEmploymentsWithIncludes();
    }
}
