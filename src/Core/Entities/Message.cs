using System;

namespace Fakebook.Core.Entities
{
	public class Message : BaseEntity
	{
		public string SenderId { get; set; }
		public User Sender { get; set; }
		public string RecieverId { get; set; }
		public User Reciever { get; set; }
		public string Content { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}