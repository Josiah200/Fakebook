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
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;
        
		public CommentController(ICommentService commentService,
					UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_commentService = commentService;
			_userManager = userManager;
			_mapper = mapper;
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

		[HttpPost("Like")]
		public async Task<IActionResult> LikeComment(string commentId)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			var likesPost = await _commentService.CheckIfUserLikesCommentAsync(commentId, currentUser.Id);
			if (!likesPost)
			{
				bool success = await _commentService.LikeCommentAsync(commentId, currentUser.Id);
				if (success)
				{
					return Ok("Liked");
				}
			}
			else
			{
				bool success = await _commentService.UnlikeCommentAsync(commentId, currentUser.Id);
				if (success)
				{
					return Ok("Unliked");
				}
			}
			
			return BadRequest();
		}
    }
}