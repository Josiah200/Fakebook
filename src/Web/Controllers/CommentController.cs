using System.Threading.Tasks;
using AutoMapper;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
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
		public async Task<IActionResult> AddComment(string postId, string text)
		{
			var currentUser = await _userManager.GetUserAsync(User);

			if (currentUser is null) 
			{
				return Challenge();
			}

			if (postId is null || string.IsNullOrEmpty(text))
			{
				return BadRequest();
			}
			
			Comment comment = new() 
			{
				PostId = postId,
				Text = text,
				UserId = currentUser.Id
			};

			await _commentService.SaveCommentAsync(comment);
			
			return RedirectToAction("Index", "Home");
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