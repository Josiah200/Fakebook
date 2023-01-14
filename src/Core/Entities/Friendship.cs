using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fakebook.Core.Entities
{
	public class Friendship : BaseEntity
	{
        public string UserId { get; set; }
		[ForeignKey("UserId")]
        public User User { get; set; }
		public string FriendId { get; set; }
		[ForeignKey("FriendId")]
		public User Friend { get; set; }
		public Status Status { get; set; }
		public DateTime Timestamp { get; set; }
		public List<Message> Messages { get; set; }
    }

	public enum Status
	{
		Pending = 0,
		Accepted = 1,
		Blocked = 2
	}
}