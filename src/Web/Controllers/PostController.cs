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
		public async Task<IActionResult> HomePosts(int page, int pageSize, string userId)
		{
			if (!this.ModelState.IsValid)
			{
				return RedirectToAction("Index", "Home");
			}
			
			var posts = await _postService.GetHomePostsPageAsync(userId, page, pageSize);

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
		public async Task<IActionResult> UserPosts(int page, int pageSize, string userId)
		{
			var posts = await _postService.GetUserPostsPageAsync(userId, page, pageSize);

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
			newPost.UserId = currentUser.Id;
			
			await _postService.SavePostAsync(newPost);
			
			return RedirectToAction("Index", "Home");
		}

		[HttpPost("Like")]
		public async Task<IActionResult> LikePost(string postId)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			var likesPost = await _postService.CheckIfUserLikes(postId, currentUser.Id);
			if (!likesPost)
			{
				bool success = await _postService.LikePostAsync(postId, currentUser.Id);
				if (success)
				{
					return Ok("Liked");
				}
			}
			else
			{
				bool success = await _postService.UnlikePostAsync(postId, currentUser.Id);
				if (success)
				{
					return Ok("Unliked");
				}
			}
			
			return BadRequest();
		}
    }
}