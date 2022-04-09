using System;
using System.Collections.Generic;

namespace Fakebook.Core.Entities
{
    public class Comment : BaseEntity
    {
		public string Text { get; set; }
        public string PostId { get; set; }
		public Post Post { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
		public DateTime DatePosted { get; set; }
		public ICollection<Like> Likes { get; set; }
		public bool IsReply { get; set; }
		public string? ParentCommentId { get; set; }
		public Comment? Parent { get; set; }
		public ICollection<Comment> Replies { get; set; }
	}
}