using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using taskCompanySystem.Identity;
using taskCompanySystem.Models;

namespace taskCompanySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login() => View();
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 1. Attempt to sign the user in
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    // 2. Check Role and Redirect
                    if (await _userManager.IsInRoleAsync(user, "Manager"))
                    {
                        // Requirement: Manager sees the full employee list
                        return RedirectToAction("ViewEmployees", "Manager");
                    }
                    else
                    {
                        // Requirement: Employee sees their daily tasks
                        return RedirectToAction("MyDailyTasks", "Employee");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

       
    }
}
