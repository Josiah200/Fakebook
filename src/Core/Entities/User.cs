using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fakebook.Core.Entities
{
	public class User : BaseEntity
	{

		[Required]
		[StringLength(18)]
		[Display(Name = "First name")]
		public string FirstName { get; set; }
		[Required]
		[StringLength(18)]
		[Display(Name = "Last name")]
		public string LastName { get; set; }
		[Required]
		[StringLength(8)]
		[Display(Name = "Public Id")]
		public string PublicId { get; set; }
		public List<Post> Posts { get; set; }
		public ICollection<Photo> Photos { get; set; }
		public byte[] ProfilePicture { get; set; }
		public ICollection<Friendship> Friendships { get; set; }
		public List<Notification> Notifications { get; set; }
		[StringLength(450)]
		public string? Bio { get; set; }
		[StringLength(10)]
		public string? Gender { get; set; }
		public DateTime? Birthdate { get; set; }
		[StringLength(40)]
		public string? City { get; set;}
		[StringLength(40)]
		public string? Hometown { get; set; }
		[StringLength(40)]
		public string? JobTitle { get; set; }
		[StringLength(40)]
		public string? Company { get; set; }
		[StringLength(40)]
		public string? HighSchool { get; set; }
		[StringLength(40)]
		public string? College { get; set; }
	}
}