using System;

namespace FakeBook.Core.Entities
{
    public class Post
    {
		//TODO: Add validation
        public Guid Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public DateTime DatePosted { get; set; }
    }
}