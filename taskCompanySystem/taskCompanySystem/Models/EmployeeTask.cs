using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using taskCompanySystem.Identity;

namespace taskCompanySystem.Models
{
    public class EmployeeTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string ImportanceLevel { get; set; }

        public string EmployeeId { get; set; }

        [ValidateNever] 
        public virtual ApplicationUser Employee { get; set; }
    }
}
