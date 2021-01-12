using System;
using Fakebook.Core.Entities;

namespace Fakebook.Web.ViewModels
{
    public class PostViewModel
    {
        public string Text { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string UserPublicId { get; set; }
		public DateTime DatePosted { get; set; }
    }
}
