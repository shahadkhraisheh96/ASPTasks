using System.ComponentModel.DataAnnotations;

namespace companySystem.Models
{
    public class employee
    {
        [Key]
        public int Id {  get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public DateOnly Birthdates {  get; set; }
        [Required,MaxLength(10)]
        public int PhoneNumber {  get; set; }
        [Required,MaxLength(20)]
        public int NationalID {  get; set; }
        [Required,MaxLength (50)]
        public string Nationalities {  get; set; }
        [Required]
        public string MaritalStatuses {  get; set; }
        public string PhotoPath { get; set; }      // Stored file path

        public DateTime EntryDate { get; set; }

        // FK → ApplicationUser (login credentials)
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        // FK → Department
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        // Navigation
        public virtual ICollection<TaskItem> Tasks { get; set; }


    }
}
