using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fakebook.Core.Entities
{
	public class User : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PublicId { get; set; }
		public ProfileData ProfileData { get; set; }
		public List<Post> Posts { get; set; }
		public ICollection<Friendship> Friendships { get; set; }
		public List<Notification> Notifications { get; set; }

	}

	public class ProfileData : BaseEntity
	{
		public bool HasAvatar { get; set; }
		public string Bio { get; set; }
		public string City { get; set;}
		public string Hometown { get; set; }
		public string Workplace { get; set; }
		public string HighSchool { get; set; }
		public string College { get; set; }

	}
}