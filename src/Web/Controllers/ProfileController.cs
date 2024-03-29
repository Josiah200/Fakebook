using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
		private readonly IMapper _mapper;
		
		public ProfileController(IUserService userService, IFriendsService friendsService, UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_userService = userService;
			_userManager = userManager;
			_friendsService = friendsService;
			_mapper = mapper;
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

			var viewModel = new ProfileViewModel
			{
				IsProfileOwner = true,
				ProfileUser = await _userService.GetByPublicIdAsync(userPublicId),
			};

			if (viewModel.ProfileUser == null)
			{
				return NotFound();
			}

			if (viewModel.ProfileUser.Id != currentUser.Id)
			{
				viewModel.IsProfileOwner = false;
				viewModel.Friendship = await _friendsService.GetFriendAsync(currentUser, viewModel.ProfileUser);
			}

			viewModel.Friends = (await _friendsService.GetFriendsListByUserIdAsync(viewModel.ProfileUser.Id)).Select(_mapper.Map<FriendViewModel>);
			return View(viewModel);
		}
    }
}