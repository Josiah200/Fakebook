using System;
using System.Collections.Generic;

namespace Fakebook.Core.Entities
{
	public class User : BaseEntity
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PublicId { get; set; }
		public List<Post> Posts { get; set; }
		public ICollection<Photo> Photos { get; set; }
		public byte[] ProfilePicture { get; set; }
		public ICollection<Friendship> Friendships { get; set; }
		public List<Notification> Notifications { get; set; }
		public string? Bio { get; set; }
		public string? Gender { get; set; }
		public DateTime? Birthdate { get; set; }
		public string? City { get; set;}
		public string? Hometown { get; set; }
		public string? JobTitle { get; set; }
		public string? Company { get; set; }
		public string? HighSchool { get; set; }
		public string? College { get; set; }
	}
}