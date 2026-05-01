using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using taskCompanySystem.Data;
using taskCompanySystem.Identity;
using Microsoft.EntityFrameworkCore;

namespace taskCompanySystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> MyDailyTasks()
        {
            var currentUserId = _userManager.GetUserId(User);

            // Filter: Tasks assigned to them specifically
            var tasks = await _db.Tasks
                .Where(t => t.EmployeeId == currentUserId && t.StartDate <= DateTime.Today && t.DueDate >= DateTime.Today)
                .ToListAsync();

            return View(tasks);
        }

    }
}
