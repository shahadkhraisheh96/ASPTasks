using System.ComponentModel.DataAnnotations;

namespace Tuple_Task.Models
{
    public class Students
    {
        [Required(ErrorMessage = "Age is required!")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100!")]
        public int Age { get; set; }
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

    }
}
