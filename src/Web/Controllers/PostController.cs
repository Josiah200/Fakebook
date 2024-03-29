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
using System.Text.Encodings.Web;
using System.Web;

namespace Fakebook.Web.Controllers
{
	[Authorize]
	[Route("[Controller]")]
    public class PostController : ControllerBase
    {
		private readonly IPostService _postService;
		private readonly ILikeService<Post> _postLikeService;
		private readonly ILikeService<Comment> _commentLikeService;
		private readonly IFriendsService _friendsService;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;

		public PostController(IPostService postService, ILikeService<Post> postLikeService,
			ILikeService<Comment> commentLikeService, IFriendsService friendsService, UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_postService = postService;
			_postLikeService = postLikeService;
			_commentLikeService = commentLikeService;
			_friendsService = friendsService;
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
			var friends = await _friendsService.GetByUserIdAsync(userId);
			var posts = await _postService.GetHomePostsPageAsync(userId, page, pageSize, friends);

			if (posts?.Any() != true)
			{
				return NotFound();
			}

			else
			{
				var postModels = new List<PostViewModel>();
				postModels.AddRange(posts.Select(_mapper.Map<PostViewModel>));
				postModels = await CheckIfUserLikesPosts(postModels, userId);
				return Ok(postModels);
			}
		}

		[HttpGet("UserPosts")]
		public async Task<IActionResult> UserPosts(int page, int pageSize, string userId)
		{
			var posts = await _postService.GetUserPostsPageAsync(userId, page, pageSize);

			if (posts?.Any() != true)
			{
				return NotFound();
			}

			else
			{
				var postModels = new List<PostViewModel>();
				postModels.AddRange(posts.Select(_mapper.Map<PostViewModel>));
				postModels = await CheckIfUserLikesPosts(postModels, userId);
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

			if (newPost is null || string.IsNullOrEmpty(newPost.Text))
			{
				return BadRequest();
			}

			newPost.UserId = currentUser.Id;
			newPost.Text = HttpUtility.HtmlEncode(newPost.Text);
			await _postService.SavePostAsync(newPost);
			
			string referrer = Request.Headers["Referer"].ToString();
			return Redirect(referrer);
		}

		[Authorize]
		[HttpPost("Like")]
		public async Task<IActionResult> Like(string postId)
		{
			var currentUser = await _userManager.GetUserAsync(User);

			var likesPost = await _postLikeService.CheckIfUserLikesAsync(postId, currentUser.Id);

			if (!likesPost)
			{
				bool success = await _postLikeService.LikeAsync(postId, currentUser.Id);
				if (success)
				{
					return Ok("Liked");
				}
			}
			else
			{
				bool success = await _postLikeService.UnlikeAsync(postId, currentUser.Id);
				if (success)
				{
					return Ok("Unliked");
				}
			}

			return BadRequest();
		}

		private async Task<List<PostViewModel>> CheckIfUserLikesPosts(List<PostViewModel> postModels, string userId)
		{
			foreach (var post in postModels)
			{
				foreach (var comment in post.Comments)
				{
					foreach (var reply in comment.Replies)
					{
						reply.UserLikes = await _commentLikeService.CheckIfUserLikesAsync(reply.Id, userId);
					}
					comment.UserLikes = await _commentLikeService.CheckIfUserLikesAsync(comment.Id, userId);
				}
				post.UserLikes = await _postLikeService.CheckIfUserLikesAsync(post.Id, userId);
			}
			return postModels;
		}
	}
}