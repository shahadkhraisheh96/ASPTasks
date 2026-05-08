using System.ComponentModel.DataAnnotations;

namespace taskEmployee.Models
{
    public class Employee
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 100 characters.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(18, 65, ErrorMessage = "Age must be between 18 and 65.")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, 100000, ErrorMessage = "Salary must be a positive value.")]
        [DataType(DataType.Currency)]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }
    }
}
