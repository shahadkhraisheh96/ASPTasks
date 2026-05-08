using Microsoft.AspNetCore.Mvc;
using taskEmployee.Models;

namespace taskEmployee.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

                return RedirectToAction("Success");
        }
        [HttpGet]
        public IActionResult Success()
        {
            return View();
        }

    }
}
