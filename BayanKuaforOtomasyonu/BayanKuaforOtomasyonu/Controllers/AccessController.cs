using BayanKuaforOtomasyonu.Models.Entities;
using BayanKuaforOtomasyonu.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BayanKuaforOtomasyonu.Controllers
{
    public class AccessController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccessController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new SignUpViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Register(SignUpViewModel userSignUp)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = userSignUp.EmailAddress, Email = userSignUp.EmailAddress, NameSurname = userSignUp.NameSurname, PhoneNumber = userSignUp.PhoneNumber };
                var result = await _userManager.CreateAsync(user, userSignUp.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home", new {area=""});
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(userSignUp);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userLogin)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    userLogin.EmailAddress,
                    userLogin.Password,
                    true,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "" });
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Hesabınız kilitlendi! 3dk sonra tekrar deneyiniz.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz email ya da şifre!");
                }
            }

            return View(userLogin);
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> AddRoles()
        {
            await _roleManager.CreateAsync(new AppRole() { Name= "Admin" });
            await _roleManager.CreateAsync(new AppRole() { Name= "Official" });
            await _roleManager.CreateAsync(new AppRole() { Name= "User" });
            return RedirectToAction("Index", "Home");
        }
    }
}
