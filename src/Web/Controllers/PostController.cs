using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Fakebook.Infrastructure.Identity;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using System.Collections.Generic;
using Fakebook.Web.Models.ViewModels;
using AutoMapper;
using System.Linq;

namespace Fakebook.Web.Controllers
{
	[Authorize]
	[Route("[Controller]")]
    public class PostController : ControllerBase
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
		
		[HttpGet("HomePosts")]
		public async Task<IActionResult> HomePosts(int page, int blockSize, string userId)
		{
			if (!this.ModelState.IsValid)
			{
				return RedirectToAction("Index", "Home");
			}
			
			var posts = await _postService.GetHomePostsPageAsync(userId, page, blockSize);

			if ((posts is null) || (!posts.Any()))
			{
				return NotFound();
			}

			else
			{
				var postModels = new List<PostViewModel>();
				postModels.AddRange(posts.Select(_mapper.Map<PostViewModel>));
				return Ok(postModels);
			}
		}
		
		[HttpGet("UserPosts")]
		public async Task<IActionResult> UserPosts(int page, int blockSize, string userId)
		{
			var posts = await _postService.GetUserPostsPageAsync(userId, page, blockSize);

			if ((posts is null) || (!posts.Any()))
			{
				return NotFound();
			}

			else
			{
				var postModels = new List<PostViewModel>();
				postModels.AddRange(posts.Select(_mapper.Map<PostViewModel>));
				return Ok(postModels);
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
				return BadRequest();
			}

			newPost.Id = Guid.NewGuid().ToString();
			newPost.UserId = currentUser.Id;
			newPost.DatePosted = DateTime.UtcNow;
			
			await _postService.SavePostAsync(newPost);
			return RedirectToAction("Index", "Home");
		}
    }
}