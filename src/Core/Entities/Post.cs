using System;
using System.ComponentModel.DataAnnotations;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Entities
{
    public class Post : BaseEntity
    {
		public int PostId { get; set; }
		public string Text { get; set; }
		public DateTime DatePosted { get; set; }

		public string AuthorId { get; set; }
		public ApplicationUser Author { get; set; }
    }
}