using System;

namespace Fakebook.Web.Models
{
    public class PostModel
    {
        public string Text { get; set; }
		public DateTime DatePosted { get; set; }
		public string UserPublicId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
    }
}