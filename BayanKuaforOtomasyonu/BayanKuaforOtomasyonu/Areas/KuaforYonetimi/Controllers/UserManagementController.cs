
using BayanKuaforOtomasyonu.Models.ViewModels.UsersViewModels;
using BayanKuaforOtomasyonu.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace BayanKuaforOtomasyonu.Areas.KuaforYonetimi.Controllers
{
    [Area("KuaforYonetimi")]
    public class UserManagementController : Controller
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            ViewBag.Roles = _userService.GetAllRoles();
            return View(users);
        }
        public async Task<IActionResult> UpdateUserRole([FromBody]AddRoleViewModel addRoleViewModel)
        {
            await _userService.ChangeRoleAsync(addRoleViewModel);
            return RedirectToAction("Index", "UserManagement", new {area= "KuaforYonetimi" });
        }
    }
}
