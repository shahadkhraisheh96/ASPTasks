using taskCompanySystem.Identity;

namespace taskCompanySystem.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ApplicationUser> Employees { get; set; }
    }
}
