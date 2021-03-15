using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fakebook.Core.Entities
{
	public class User : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PublicId { get; set; }
		public List<Post> Posts { get; set; }
		public ICollection<Friendship> Friendships { get; set; }
		public List<Notification> Notifications { get; set; }
	}
}