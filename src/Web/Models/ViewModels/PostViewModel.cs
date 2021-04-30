using System;

namespace Fakebook.Web.Models.ViewModels
{
    public class PostViewModel
    {
		public string Id { get; set; }
        public string Text { get; set; }
		public DateTime DatePosted { get; set; }
		public string UserPublicId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public byte[] ProfilePicture { get; set; }
		public int Likes { get; set; }
    }
}