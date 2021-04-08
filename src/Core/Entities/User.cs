using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fakebook.Core.Entities
{
	public class User : BaseEntity
	{

		[Required]
		[StringLength(12)]
		public string FirstName { get; set; }
		[Required]
		[StringLength(16)]
		public string LastName { get; set; }
		[Required]
		[StringLength(8)]
		public string PublicId { get; set; }
		public List<Post> Posts { get; set; }
		public ICollection<Friendship> Friendships { get; set; }
		public List<Notification> Notifications { get; set; }
		[Required]
		public bool HasAvatar { get; set; }
		[StringLength(400)]
		public string? Bio { get; set; }
		[StringLength(10)]
		public string? Gender { get; set; }
		public DateTime? Birthdate { get; set; }
		[StringLength(20)]
		public string? City { get; set;}
		[StringLength(20)]
		public string? State { get; set; }
		[StringLength(20)]
		public string? Country { get; set; }
		[StringLength(20)]
		public string? HometownCity { get; set; }
		[StringLength(20)]
		public string? HometownState { get; set; }
		[StringLength(20)]
		public string? HometownCountry { get; set; }
		[StringLength(20)]
		public string? JobTitle { get; set; }
		[StringLength(20)]
		public string? Company { get; set; }
		[StringLength(20)]
		public string? HighSchool { get; set; }
		[StringLength(20)]
		public string? College { get; set; }
	}
}