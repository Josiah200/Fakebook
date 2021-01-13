using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Fakebook.Core.Interfaces;

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
		public async Task<ActionResult> PostScroll(int page, int blocksize)
		{
			var posts = await _repository.GetHomePostsAsync(page, blocksize);
			return PartialView("_PostsPagePartial", posts);
		}
    }
}