using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Fakebook.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System;
using Fakebook.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Fakebook.Infrastructure.Identity;
using Fakebook.Core.Entities;

namespace Fakebook.Web.Controllers
{
	[Route("[Controller]")]
    public class PostController : Controller
    {
		private readonly IPostService _postService;
		private readonly UserManager<ApplicationUser> _userManager;
        
		public PostController(IPostService postService, UserManager<ApplicationUser> userManager)
		{
			_postService = postService;
			_userManager = userManager;
		}

		[HttpGet("PostScroll")]
		[Authorize]
		public async Task<ActionResult> PostScroll(int page, int blocksize)
		{
			try 
			{
				var posts = await _postService.GetPostsBlockAsync(page, blocksize);
				return PartialView("_PostsPagePartial", posts);
			}
			catch(Exception ex)
			{
				return Content("Error: " + ex.Message);
			}
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> NewPost(NewPostViewModel newPost)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser == null) return Challenge();
			bool successful = false;
			var post = new Post
			{
				Id = Guid.NewGuid().ToString(),
				UserId = currentUser.Id,
				Text = newPost.Text,
				DatePosted = DateTime.Now
			};
			
			if (!string.IsNullOrEmpty(post.Text))
			{
				successful = await _postService.SavePostAsync(post);
			}

			if (successful)
			{
				return RedirectToAction("Index", "Home");
			}
			
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
    }
}