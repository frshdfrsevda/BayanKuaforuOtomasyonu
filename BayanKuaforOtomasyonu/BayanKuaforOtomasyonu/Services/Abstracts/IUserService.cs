using BayanKuaforOtomasyonu.Models.ViewModels.UsersViewModels;

namespace BayanKuaforOtomasyonu.Services.Abstracts
{
    public interface IUserService
    {
        Task ChangeRoleAsync(AddRoleViewModel addRoleViewModel);
        Task<List<UserListViewModel>> GetAllUsersAsync();
        List<RoleViewModel> GetAllRoles();
    }
}
