using System.ComponentModel.DataAnnotations;

namespace companySystem.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(100)]
        public string Name { get; set; }
        public virtual ICollection<employee> Employees { get; set; }
    }
}
