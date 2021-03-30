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
	[Authorize]
	[Route("[Controller]")]
    public class PostController : Controller
    {
		private readonly IPostService _postService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;
        
		public PostController(IPostService postService, UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_postService = postService;
			_userManager = userManager;
			_mapper = mapper;
		}
		
		/// <summary>
		/// Gets blocksize posts from friends if userPublicId is null, user's posts otherwise.
		/// </summary>
		[HttpGet("HomePosts")]
		public async Task<IActionResult> HomePosts(int page, int blockSize)
		{
			if (!this.ModelState.IsValid)
			{
				return RedirectToAction("Index", "Home");
			}
			var currentUserId = (await _userManager.GetUserAsync(User)).Id;
			var posts = await _postService.GetHomePostsBlockAsync(page, blockSize, currentUserId);

			if (!posts.Any())
			{
				return NotFound();
			}

			else
			{
				var postModels = new List<PostModel>();
				postModels.AddRange(posts.Select(_mapper.Map<PostModel>));
				return PartialView("_PostsPagePartial", postModels);
			}
		}
		[HttpGet("UserPosts")]
		public async Task<IActionResult> UserPosts(int page, int blockSize, string userPublicId)
		{
			var posts = await _postService.GetUserPostsBlockAsync(page, blockSize, userPublicId);

			if (!posts.Any())
			{
				return NotFound();
			}

			else
			{
				var postModels = new List<PostModel>();
				postModels.AddRange(posts.Select(_mapper.Map<PostModel>));
				return PartialView("_PostsPagePartial", postModels);
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