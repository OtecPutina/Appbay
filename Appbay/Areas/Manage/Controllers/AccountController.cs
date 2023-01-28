using Appbay.Areas.Manage.ViewModel;
using Appbay.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Appbay.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public UserManager<AppUser> _userManager { get; }
        public SignInManager<AppUser> _signInManager { get; }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel adminLoginViewModel)
        {
            AppUser admin=await _userManager.FindByNameAsync(adminLoginViewModel.UserName);
            if (admin==null)
            {
                ModelState.AddModelError("", "Username or Password is invalid");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(admin, adminLoginViewModel.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is invalid");
                return View();
            }
            return RedirectToAction("Index", "Dashboard");
        }
        public async Task<IActionResult> Logout()
        {
            AppUser appUser = await _userManager.FindByNameAsync("admin");
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}
