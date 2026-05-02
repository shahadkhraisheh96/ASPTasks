using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using taskIdentity.Identity;

namespace taskIdentity.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> ListUser()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> PromoteToAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // AddUserToRole checks if they are already in it, then adds the
                var result = await _userManager.AddToRoleAsync(user, "Admin");
                if (result.Succeeded)
                {
                    TempData["Message"] = $"{user.FullName} has been promoted to Admin!";
                }
            }
            return RedirectToAction("ListUser");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveAdminRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                // Check if the user is actually an Admin before trying to remove it
                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

                if (isAdmin)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
                    if (result.Succeeded)
                    {
                        TempData["Message"] = $"{user.FullName} is no longer an Admin.";
                    }
                }
            }

            return RedirectToAction("ListUser");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
