using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Controllers
{
	[Route("[Controller]")]
	[ApiController]
    public class PostController : Controller
    {
		private readonly IPostRepository _repository;
        
		public PostController(IPostRepository postRepository)
		{
			_repository = postRepository;
		}

		[HttpGet]
		public async Task<ActionResult> PostScroll(int page)
		{
			var posts = await _repository.GetHomePostsAsync(page);
			var model = new List<PostViewModel>();
			foreach (Post post in posts)
			{
				model.Add(new PostViewModel
				{
					Text = post.Text,
					FirstName = post.User.FirstName,
					LastName = post.User.LastName,
					UserPublicId = post.User.PublicId,
					DatePosted = post.DatePosted
				});
			}
			return PartialView("_PostsPagePartial", model);
		}
    }
}