using System;
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
    }
}