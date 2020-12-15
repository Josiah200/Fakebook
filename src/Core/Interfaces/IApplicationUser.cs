using System.Collections.Generic;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IApplicationUser
    {
    	string FirstName { get; set; }
		string LastName { get; set; }
		List<Post> Posts { get; set; }
    }
}