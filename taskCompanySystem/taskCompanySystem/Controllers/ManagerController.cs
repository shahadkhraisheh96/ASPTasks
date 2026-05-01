using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using taskCompanySystem.Data;
using taskCompanySystem.Identity;
using taskCompanySystem.Models;

namespace taskCompanySystem.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManagerController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        // GET: Manager/ViewEmployees
        // Requirement: Filter employees by name & view all details
        public IActionResult ViewEmployees(string searchName)
        {
            var employees = _db.Users.Include(u => u.Department).AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                employees = employees.Where(e => e.FullName.Contains(searchName));
            }

            return View(employees.ToList());
        }

        // GET: Manager/CreateEmployee
        [HttpGet]
        public async Task<IActionResult> CreateEmployee()
        {
            // Requirement: Populate Department dropdown for the View[cite: 24, 30]
            ViewBag.Departments = new SelectList(await _db.Departments.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Manager/CreateEmployee
        // Requirement: Add new employees[cite: 24, 30]
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(ApplicationUser model, string password, IFormFile profilePhoto)
        {
            // 1. Manually set fields that aren't in the form
            model.EntryDate = DateTime.Now;
            model.UserName = model.Email; // Identity requires a UserName[cite: 21, 44]

            // 2. Handle the Photo upload[cite: 30]
            if (profilePhoto != null)
            {
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid().ToString() + "_" + profilePhoto.FileName;
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePhoto.CopyToAsync(stream);
                }
                model.PhotoPath = "/uploads/" + fileName;
            }
            else
            {
                model.PhotoPath = "/uploads/default.png"; // Default value to satisfy database[cite: 3, 8]
            }

            // 3. Clear validation for navigation properties we handle manually[cite: 3, 24]
            ModelState.Remove("Department");
            ModelState.Remove("Tasks");
            ModelState.Remove("profilePhoto");

            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(model, password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(model, "Employee");
                    return RedirectToAction("ViewEmployees");
                }
                foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            }

            // Reload departments if validation fails
            ViewBag.Departments = new SelectList(await _db.Departments.ToListAsync(), "Id", "Name");
            return View(model);
        }
        // GET: Manager/AssignTask
        [HttpGet]
        public async Task<IActionResult> AssignTask()
        {
            // Requirement: Populate Employee dropdown for task assignment[cite: 24, 29]
            var employees = await _userManager.Users
                .Select(u => new { u.Id, u.FullName })
                .ToListAsync();

            ViewBag.EmployeeList = new SelectList(employees, "Id", "FullName");

            return View();
        }

        // POST: Manager/AssignTask
        // Requirement: Assign tasks to specific employees[cite: 18, 24]
        [HttpPost]
        public async Task<IActionResult> AssignTask(EmployeeTask task)
        {
            // Remove the navigation property from validation so it doesn't block the save[cite: 3, 24]
            ModelState.Remove("Employee");

            if (ModelState.IsValid)
            {
                _db.Tasks.Add(task);
                await _db.SaveChangesAsync(); // This will work now as only EmployeeId is required[cite: 18]
                return RedirectToAction("ViewEmployees");
            }

            // If we reach here, validation failed; reload the dropdown list[cite: 24, 29]
            var employees = await _userManager.Users
                .Select(u => new { u.Id, u.FullName })
                .ToListAsync();
            ViewBag.EmployeeList = new SelectList(employees, "Id", "FullName");

            return View(task);
        }

        // GET: Manager/Feedback
        // Requirement: View Contact Us feedback ordered by date[cite: 16, 24, 31]
        public IActionResult Feedback()
        {
            return View(_db.ContactFeedbacks.OrderByDescending(f => f.SubmittedAt).ToList());
        }
    }
}