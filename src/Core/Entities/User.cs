using System.Collections.Generic;
using Fakebook.Core.Interfaces;

namespace Fakebook.Core.Entities
{
	public class User : BaseEntity
	{
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public List<Post> Posts { get; set; }
	}
}