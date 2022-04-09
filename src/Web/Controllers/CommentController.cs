using System;
using System.Threading.Tasks;
using AutoMapper;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Fakebook.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Controllers
{
	[Authorize]
	[Route("[Controller]")]
    public class CommentController : ControllerBase
    {	
		private readonly ICommentService _commentService;
		private readonly ILikeService<Comment> _likeService;
		private readonly UserManager<ApplicationUser> _userManager;
        
		public CommentController(ICommentService commentService, 
			ILikeService<Comment> likeService, UserManager<ApplicationUser> userManager)
		{
			_commentService = commentService;
			_likeService = likeService;
			_userManager = userManager;
		}
		
		[HttpPost]
		public async Task<IActionResult> AddComment([FromForm] NewCommentModel commentInput)
		{
			var currentUser = await _userManager.GetUserAsync(User);

			if (currentUser is null) 
			{
				return Challenge();
			}

			if (commentInput.PostId is null || string.IsNullOrEmpty(commentInput.Text))
			{
				return BadRequest();
			}
			
			Comment comment = new() 
			{
				PostId = commentInput.PostId,
				Text = commentInput.Text,
				UserId = currentUser.Id
			};
			
			if (!string.IsNullOrEmpty(commentInput.CommentId))
			{
				comment.IsReply = true;
				comment.ParentCommentId = commentInput.CommentId;
			}
			
			await _commentService.SaveCommentAsync(comment);

			string referrer = Request.Headers["Referer"].ToString();
			return Redirect(referrer);
		}

		[Authorize]
		[HttpPost("Like")]
		public async Task<IActionResult> Like(string commentId)
		{
			var currentUser = await _userManager.GetUserAsync(User);

			var likesPost = await _likeService.CheckIfUserLikesAsync(commentId, currentUser.Id);

			if (!likesPost)
			{
				bool success = await _likeService.LikeAsync(commentId, currentUser.Id);
				if (success)
				{
					return Ok("Liked");
				}
			}
			else
			{
				bool success = await _likeService.UnlikeAsync(commentId, currentUser.Id);
				if (success)
				{
					return Ok("Unliked");
				}
			}

			return BadRequest();
		}
    }
}