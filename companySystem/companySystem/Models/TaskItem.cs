using System.ComponentModel.DataAnnotations;

namespace companySystem.Models
{
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public string Description { get; set; }

        public string ImportanceLevel { get; set; }

        // FK → Employee
        public int AssignedToEmployeeId { get; set; }
        public virtual employee AssignedEmployee { get; set; }

    }
}