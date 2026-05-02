using Microsoft.AspNetCore.Identity;

namespace taskIdentity.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }

    }
}
