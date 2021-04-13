using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Fakebook.Web.Models;
using Fakebook.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
		private readonly IPhotoService _photoService;
		
		public ProfileController(IUserService userService, IFriendsService friendsService, UserManager<ApplicationUser> userManager, IPhotoService photoService)
		{
			_userService = userService;
			_userManager = userManager;
			_friendsService = friendsService;
			_photoService = photoService;
		}

		[BindProperty]
        public User UpdateInput { get; set; }


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
		[Route("UpdateProfile")]

		public async Task<IActionResult> UpdateProfile()
		{
			var currentApplicationUser = await _userManager.GetUserAsync(User);
			var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);
			var successful = await _userService.UpdateProfileAsync(currentUser, UpdateInput);
			if (successful)
			{
				return Content($"<h5>Updated</h5>");
			}
			else
			{
				return Content($"<h5>Internal error, please reload and try again<h5>");
			}
		}
    }
}