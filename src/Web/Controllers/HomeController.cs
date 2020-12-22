using Fakebook.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Fakebook.Core.Entities;
using System.Threading.Tasks;
using Fakebook.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Fakebook.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Fakebook.Web.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IPostService _postService;
		private readonly IUserService _userService;
		private readonly IPostRepository _repository;
		
		public HomeController(UserManager<ApplicationUser> userManager, IPostService postService, IUserService userService, IPostRepository repository)
		{
			_userManager = userManager;
			_postService = postService;
			_repository = repository;
			_userService = userService;
		}

    	public async Task<IActionResult> Index()
		{
			
			var viewModel = new HomeViewModel
			{
				Posts = await _repository.GetHomePostsAsync(),
				CurrentUser = await _userManager.GetUserAsync(User)
			};
			//View(await _repository.ListAllAsync());
			return View(viewModel);
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> NewPost(NewPostViewModel newPost)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser == null) return Challenge();

			bool successful = await _postService.NewPost(newPost.Text, currentUser.Id);
			if (successful)
			{
				return RedirectToAction("Index");
			}
			else
			{
				return RedirectToAction("Index");
			}
		}
    }
}
