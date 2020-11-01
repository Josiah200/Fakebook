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
		private readonly IPostRepository _repository;
		private readonly UserManager<ApplicationUser> _userManager;
		
		public HomeController(IPostRepository repository, 
			UserManager<ApplicationUser> userManager)
		{
			_repository = repository;
			_userManager = userManager;
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

		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<IActionResult> NewPost(Post newPost)
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser == null) return Challenge();
			
			newPost.AuthorId = currentUser.Id;
			newPost.AuthorFirst = currentUser.FirstName;
			newPost.AuthorLast = currentUser.LastName;

			if (!ModelState.IsValid)
			{
				return RedirectToAction("Index");
			}

			var successful = await _repository.AddAsync(newPost);
			if (!successful)
			{
				return BadRequest("Could not create post.");
			}
			return RedirectToAction("Index");
		}
    }
}