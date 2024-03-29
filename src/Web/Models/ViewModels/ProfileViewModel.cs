using System.Collections.Generic;
using Fakebook.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Models.ViewModels
{
    public class ProfileViewModel
    {
		public User ProfileUser { get; set; }
		public bool IsProfileOwner { get; set; }
		public Friendship? Friendship { get; set; }
		public UserProfileUpdateModel UpdateInput { get; set; }
		public NewPublicIdModel PublicIdInput { get; set; }
		public NewPhotoModel PhotoInput { get; set; }
		public NewCommentModel CommentInput { get; set; }
		public IEnumerable<FriendViewModel> Friends { get; set; }
	}
}