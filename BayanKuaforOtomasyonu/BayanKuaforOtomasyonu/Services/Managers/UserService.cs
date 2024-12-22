using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Models.ViewModels.UsersViewModels;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Data;

namespace BayanKuaforOtomasyonu.Services.Managers
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task ChangeRoleAsync(AddRoleViewModel addRoleViewModel)
        {
            var user = await _userManager.FindByIdAsync(addRoleViewModel.UserId);

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.Count > 0)
            {
                await _userManager.RemoveFromRolesAsync(user, userRoles);
            }
            await _userManager.AddToRoleAsync(user, addRoleViewModel.RoleName);
        }

        public List<RoleViewModel> GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            var listModel = new List<RoleViewModel>();
            foreach (var role in roles)
            {
                var model = new RoleViewModel() { RoleName=role.Name};
                listModel.Add(model);
            }
            return listModel;
        }

        public async Task<List<UserListViewModel>> GetAllUsersAsync()
        {
            var users = _userManager.Users.ToList();
            var listModel = new List<UserListViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var model = new UserListViewModel()
                {
                    NameSurname = user.NameSurname,
                    PhoneNumber = user.PhoneNumber,
                    EmailAddress = user.Email,
                    Id = user.Id,
                    RoleName = roles.SingleOrDefault()
                };
                listModel.Add(model);
            }
            return listModel;
        }
    }
}
