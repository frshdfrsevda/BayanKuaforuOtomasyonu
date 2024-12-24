using BayanKuaforOtomasyonu.Models.ViewModels.AppUserEmploymentViewModel;

namespace BayanKuaforOtomasyonu.Services.Abstracts
{
    public interface IUserEmploymentService
    {
        List<UserEmploymentsViewModel> GetAllUserEmployments(string userId);
        void CreateOrUpdateUserEmployment(UserEmploymentViewModel model);
        void DeleteUserEmployment(int id);
    }
}
