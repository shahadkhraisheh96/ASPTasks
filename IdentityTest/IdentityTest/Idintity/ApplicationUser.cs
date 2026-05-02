using Microsoft.AspNetCore.Identity;

namespace IdentityTest.Idintity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }

    }
}
