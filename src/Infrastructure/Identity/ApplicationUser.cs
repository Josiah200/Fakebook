using System.Collections.Generic;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Fakebook.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser, IApplicationUser
    {
		// public override string Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
    }
}
