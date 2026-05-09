using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tuple_Task.Models;

namespace Tuple_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Students()
        {
            Students stu1 = new Students { Id = 1, Name = "John Doe", Age = 20 };
            Students stu2 = new Students { Id = 2, Name = "Jane Smith", Age = 22 };
            Students[] studentsarr = new Students[] { stu1, stu2 };
            Courses course1 = new Courses { Id = 1, Name = "Mathematics", InstructorName = "Dr. Smith" };
            Courses course2 = new Courses { Id = 2, Name = "Physics", InstructorName = "Dr. Johnson" };
            Courses[] coursesarr = new Courses[] { course1, course2 };
            var tuple = (Students: studentsarr, Courses: coursesarr);

            return View(tuple);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
