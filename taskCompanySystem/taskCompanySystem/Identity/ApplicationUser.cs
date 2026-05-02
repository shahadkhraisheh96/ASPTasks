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
        public string? PhotoPath { get; set; }

        public DateTime EntryDate { get; set; }
        public int DepartmentId { get; set; }

        [ValidateNever] 
        public virtual Department Department { get; set; }

        [ValidateNever] 
        public virtual ICollection<EmployeeTask> Tasks { get; set; }
    }
}
