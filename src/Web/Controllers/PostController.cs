using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Fakebook.Infrastructure.Identity;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using System.Collections.Generic;

namespace Fakebook.Web.Controllers
{
	[Route("[Controller]")]
    public class PostController : Controller
    {
		private readonly IPostService _postService;
		private readonly IUserService _userService;
		private readonly UserManager<ApplicationUser> _userManager;
        
		public PostController(IPostService postService, IUserService userService, UserManager<ApplicationUser> userManager)
		{
			_postService = postService;
			_userService = userService;
			_userManager = userManager;
		}
		
		/// <summary>
		/// Gets blocksize posts from friends if userPublicId is null, user's posts otherwise.
		/// </summary>
		[HttpGet("PostScroll")]
		[Authorize]
		public async Task<ActionResult> PostScroll(int page, int blockSize, string? userPublicId)
		{
			if (!this.ModelState.IsValid)
			{
				return RedirectToAction("Index", "Home");
			}

			if (userPublicId != null)
			{
				var profileUser = await _userService.GetByPublicIdAsync(userPublicId);

				var posts = await _postService.GetUserPostsBlockAsync(page, blockSize, profileUser.Id);

				return ReturnPosts(posts);
			}

			else
			{
				var currentApplicationUser = await _userManager.GetUserAsync(User);
				var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);

				var posts = await _postService.GetFriendsPostsBlockAsync(page, blockSize, currentUser.Id);
				
				return ReturnPosts(posts);
			}
		}

		private ActionResult ReturnPosts(List<Post>? posts)
		{
			if (posts != null)
			{
				return PartialView("_PostsPagePartial", posts);
			}
			else
			{
				return Ok();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> NewPost(Post newPost)
		{
			
			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser is null) 
			{
				return Challenge();
			}

			if (newPost is null)
			{
				throw new ArgumentNullException(nameof(newPost));
			}

			if (string.IsNullOrEmpty(newPost.Text))
			{
				return RedirectToAction("Index", "Home");
			}

			newPost.Id = Guid.NewGuid().ToString();
			newPost.UserId = currentUser.Id;
			newPost.DatePosted = DateTime.Now;
			
			await _postService.SavePostAsync(newPost);
			return RedirectToAction("Index", "Home");
		}
    }
}