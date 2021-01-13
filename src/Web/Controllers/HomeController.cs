using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Fakebook.Web.ViewModels;

namespace Fakebook.Web.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IPostService _postService;
		
		public HomeController(UserManager<ApplicationUser> userManager, IPostService postService)
		{
			_userManager = userManager;
			_postService = postService;
		}

    	public async Task<IActionResult> Index()
		{
			var viewModel = new HomeViewModel
			{
				CurrentUser = await _userManager.GetUserAsync(User)
			};

			return View(viewModel);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> NewPost(NewPostViewModel newPost)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser == null) return Challenge();

			bool successful = await _postService.NewPostAsync(newPost.Text, currentUser.Id);

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
