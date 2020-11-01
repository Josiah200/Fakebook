using System;
using System.ComponentModel.DataAnnotations;

namespace Fakebook.Core.Entities
{
    public class Post : BaseEntity
    {
		//TODO: Add validation
		public int PostId { get; set; }
		public string Text { get; set; }
		public string AuthorId { get; set; }
		public string AuthorFirst { get; set; }
		public string AuthorLast { get; set; }
		public DateTime DatePosted { get; set; }
		public Post()
		{
			this.DatePosted = DateTime.Now;
		}
    }
}