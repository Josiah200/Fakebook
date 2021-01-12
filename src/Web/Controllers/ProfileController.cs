using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Fakebook.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Controllers
{
	[Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly IPostService _postService;
		private readonly IPostRepository _postRepository;
		private readonly IUserRepository _userRepository;

		public ProfileController(UserManager<ApplicationUser> userManager, IPostService postService, IPostRepository postRepository, IUserRepository userRepository)
		{
			_userManager = userManager;
			_postService = postService;
			_postRepository = postRepository;
			_userRepository = userRepository;
		}

		[Route("Profile/{userPublicId:int}")]
		public async Task<IActionResult> Index(string userPublicId)
		{
			var profileUser = await _userRepository.GetByPublicIdAsync(userPublicId);

			if (profileUser == null)
			{
				return RedirectToAction("Index");
			}

			var viewModel = new ProfileViewModel
			{
				Posts = await _postRepository.GetUserPostsAsync(profileUser.Id)
			};
			return View(viewModel);
		}
    }
}