using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Entities
{
    public class Post : BaseEntity
    {
		[Required]
		public string Text { get; set; }
		public DateTime DatePosted { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
		public ICollection<Like> Likes { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public string? PhotoId { get; set; }
		public Photo? Photo { get; set; }
    }
}