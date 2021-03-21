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
		private readonly IUserService _userService;
		private readonly IFriendsService _friendsService;

		public ProfileController(IUserService userService, IFriendsService friendsService, UserManager<ApplicationUser> userManager)
		{
			_userService = userService;
			_userManager = userManager;
			_friendsService = friendsService;
		}

		[Route("Profile/{userPublicId:int?}")]
		public async Task<IActionResult> Index(string? userPublicId)
		{
			var currentApplicationUser = await _userManager.GetUserAsync(User);
			var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);

			if (userPublicId is null)
			{
				userPublicId = currentUser.PublicId;
			}

			var profileUser = await _userService.GetByPublicIdAsync(userPublicId);
			if (profileUser == null)
			{
				return NotFound();
			}

			var friendship = await _friendsService.GetFriendAsync(currentUser, profileUser);
			var viewModel = new ProfileViewModel
			{
				ProfileUser = profileUser,
				Friendship = friendship
			};
			return View(viewModel);
		}
    }
}