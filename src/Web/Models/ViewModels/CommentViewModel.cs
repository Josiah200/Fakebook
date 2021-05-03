using System;

namespace Fakebook.Web.Models.ViewModels
{
    public class CommentViewModel
    {
		public string Id { get; set; }
        public string Text { get; set; }
		public string Author { get; set; }
		public string AuthorPublicId { get; set; }
		public byte[] ProfilePicture { get; set; }
		public DateTime DatePosted { get; set; }
		public int Likes { get; set; }
		public bool UserLikes { get; set; }
    }
}