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