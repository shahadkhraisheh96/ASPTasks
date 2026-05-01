using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using taskCompanySystem.Models;
namespace taskCompanySystem.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string NationalID { get; set; }
        public string Nationality { get; set; }
        public string MaritalStatus { get; set; }

        // Change: Ensure this is nullable or use [ValidateNever]
        public string? PhotoPath { get; set; }

        public DateTime EntryDate { get; set; }
        public int DepartmentId { get; set; }

        [ValidateNever] // Add this: Stops the "Department field is required" error[cite: 3, 15]
        public virtual Department Department { get; set; }

        [ValidateNever] // Add this: Stops the "Tasks field is required" error[cite: 15, 18]
        public virtual ICollection<EmployeeTask> Tasks { get; set; }
    }
}
