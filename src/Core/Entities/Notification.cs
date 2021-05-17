using System;

namespace Fakebook.Core.Entities
{
	public class Notification : BaseEntity
	{
		public string ReceiverId { get; set; }
		public User Receiver { get; set; }
		public DateTime TimeSent { get; set; }
	}
}