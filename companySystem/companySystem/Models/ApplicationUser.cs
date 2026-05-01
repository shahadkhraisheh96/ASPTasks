using Microsoft.AspNetCore.Identity;

namespace companySystem.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Role { get; set; }
    }
}
