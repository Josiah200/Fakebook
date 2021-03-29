using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Fakebook.Infrastructure.Identity;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using System.Collections.Generic;
using Fakebook.Web.Models;
using AutoMapper;
using System.Linq;

namespace Fakebook.Web.Controllers
{
	[Route("[Controller]")]
    public class PostController : Controller
    {
		private readonly IPostService _postService;
		private readonly IUserService _userService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;
        
		public PostController(IPostService postService, IUserService userService, UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_postService = postService;
			_userService = userService;
			_userManager = userManager;
			_mapper = mapper;
		}
		
		/// <summary>
		/// Gets blocksize posts from friends if userPublicId is null, user's posts otherwise.
		/// </summary>
		[HttpGet("PostScroll")]
		[Authorize]
		public async Task<IActionResult> PostScroll(int page, int blockSize, string? userPublicId)
		{
			if (!this.ModelState.IsValid)
			{
				return RedirectToAction("Index", "Home");
			}
			IEnumerable<Post> posts;

			if (userPublicId != null)
			{
				var profileUser = await _userService.GetByPublicIdAsync(userPublicId);

				posts = await _postService.GetUserPostsBlockAsync(page, blockSize, profileUser.Id);
			}

			else
			{
				var currentApplicationUser = await _userManager.GetUserAsync(User);
				var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);

				posts = await _postService.GetUserPostsBlockAsync(page, blockSize, currentUser.Id);
			}

			if (posts != null)
			{
				var postModels = new List<PostModel>();
				postModels.AddRange(posts.Select(_mapper.Map<PostModel>));
				return PartialView("_PostsPagePartial", postModels);
			}

			else
			{
				return NotFound();
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