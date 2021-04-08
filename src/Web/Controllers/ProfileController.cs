using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Fakebook.Web.Models;
using Fakebook.Web.Models.ViewModels;
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

		[BindProperty]
        public User UpdateInput { get; set; }
		
		public ProfileController(IUserService userService, IFriendsService friendsService, UserManager<ApplicationUser> userManager)
		{
			_userService = userService;
			_userManager = userManager;
			_friendsService = friendsService;
		}
		[HttpGet]
		[Route("Profile/{userPublicId?}")]
		public async Task<IActionResult> Index(string? userPublicId)
		{
			var currentApplicationUser = await _userManager.GetUserAsync(User);
			var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);

			if (userPublicId is null)
			{
				return RedirectToAction("Index", "Profile", new { userPublicId = currentUser.PublicId });
			}

			var viewModel = new ProfileViewModel()
			{
				IsProfileOwner = true
			};

			viewModel.ProfileUser = await _userService.GetByPublicIdAsync(userPublicId);

			if (viewModel.ProfileUser == null)
			{
				return NotFound();
			}

			if (viewModel.ProfileUser.Id != currentUser.Id)
			{
				viewModel.IsProfileOwner = false;
				viewModel.Friendship = await _friendsService.GetFriendAsync(currentUser, viewModel.ProfileUser);
				
			}
			
			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateProfile()
		{
			var currentApplicationUser = await _userManager.GetUserAsync(User);
			var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);
			var successful = await _userService.UpdateProfileAsync(currentUser, UpdateInput);
			return Ok(successful);
		}
    }
}