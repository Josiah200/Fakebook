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
		private readonly IPostRepository _repository;
		
		public HomeController(UserManager<ApplicationUser> userManager, IPostService postService, IPostRepository repository)
		{
			_userManager = userManager;
			_postService = postService;
			_repository = repository;
		}

    	public async Task<IActionResult> Index()
		{
			var viewModel = new HomeViewModel
			{
				Posts = await _repository.ListAllAsync(),
				CurrentUser = await _userManager.GetUserAsync(User)
			};
			//View(await _repository.ListAllAsync());
			return View(viewModel);
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> NewPost(NewPostViewModel newPost)
		//todo: rework fronend connection: add form viewmodel? add error handling? should service return something? delete most of the below. Add userrepository?
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser == null) return Challenge();

			await _postService.NewPost(newPost.Text, currentUser.Id, currentUser);
			return RedirectToAction("Index");
		}
    }
}