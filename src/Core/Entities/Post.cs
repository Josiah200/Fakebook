using System;

namespace Fakebook.Core.Entities
{
    public class Post
    {
		//TODO: Add validation
		public int PostId { get; set; }
		public int AuthorId { get; set; }
		public string Text { get; set; }
		public DateTime DatePosted { get; set; }
    }
}