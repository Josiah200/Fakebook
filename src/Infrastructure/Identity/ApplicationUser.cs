using System.Collections.Generic;
using Fakebook.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Fakebook.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
		// public override string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<Post> Posts { get; set; }
    }
}
