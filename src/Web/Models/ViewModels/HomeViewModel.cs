using System.Collections.Generic;
using Fakebook.Core.Entities;
using Fakebook.Infrastructure.Identity;

namespace Fakebook.Web.Models.ViewModels
{
    public class HomeViewModel
	{
		public string CurrentUserId { get; set; }
		public NewCommentModel CommentInput { get; set; }
	}
}