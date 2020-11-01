using Microsoft.AspNetCore.Identity;

namespace Fakebook.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
		public string FirstName { get; set; }
		public string LastName { get; set; }
    }
}
