using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fakebook.Core.Entities
{
	public class Friendship : BaseEntity, IDisposable
	{
        public string UserId { get; set; }
		[ForeignKey("UserId")]
        public User User { get; set; }
		public string FriendId { get; set; }
		[ForeignKey("FriendId")]
		public User Friend { get; set; }
		public Status Status { get; set; }
		public DateTime Timestamp { get; set; }
		public void Dispose() {}
    }

	public enum Status
	{
		Pending = 0,
		Accepted = 1,
		Blocked = 2
	}
}