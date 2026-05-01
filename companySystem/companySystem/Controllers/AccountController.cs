using companySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace companySystem.Controllers
{
    public class AccountController(SignInManager<ApplicationUser> signInManager,
                               UserManager<ApplicationUser> userManager) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                return user!.Role == "Manager" ? RedirectToAction("Index", "Manager") : RedirectToAction("Dashboard", "Employee");
            }
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
