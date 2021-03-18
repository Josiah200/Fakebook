using Microsoft.AspNetCore.Mvc;
using Fakebook.Core.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fakebook.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Fakebook.Infrastructure.Identity;
using System.Security.Claims;
using Fakebook.Core.Services;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.Extensions.Logging;

namespace Fakebook.Web.Controllers
{
	[ApiController]
	[Route("[Controller]")]
	public class FriendsController : Controller
    {
		private readonly IFriendsService _friendsService; 
		private readonly IUserService _userService;
		private readonly UserManager<ApplicationUser> _userManager;

	    public FriendsController(IFriendsService friendsService, IUserService userService, UserManager<ApplicationUser> userManager)
		{
			_friendsService = friendsService;
			_userService = userService;
			_userManager = userManager;
		}

		[HttpGet("Requests")]
		[Authorize]
		public async Task<ActionResult> GetFriendRequests()
		{
			var currentUser = await _userManager.GetUserAsync(User);
			var requests = await _friendsService.GetIncomingRequestsByUserIdAsync(currentUser.Id);
			var viewModel = PartialView("_FriendRequestsChunkPartial", requests);
			return viewModel;
		}
		
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> AddFriend()
		{
			var currentApplicationUser = await _userManager.GetUserAsync(User);

			if (currentApplicationUser is null)
			{
				return Challenge();
			}

			var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);

			var reciever = await _userService.GetByPublicIdAsync(HttpContext.Request.Form["userPublicId"]);

			if (currentUser == reciever | currentUser is null | reciever is null)
			{
				return BadRequest();
			}

			await _friendsService.SendRequestAsync(currentUser, reciever);
			return Redirect(Request.Headers["Referer"].ToString());
		}
		[HttpGet("Remove")]
		[Authorize]
		public async Task<IActionResult> RemoveFriend(string userPublicId)
		{
			var currentApplicationUser = await _userManager.GetUserAsync(User);
			if (currentApplicationUser == null) return Challenge();
			var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);
			var reciever = await _userService.GetByPublicIdAsync(userPublicId);
			bool successful = await _friendsService.RemoveFriendAsync(currentUser, reciever);

			if (successful)
			{
				return Redirect(Request.Headers["Referer"].ToString());
			}
			
			else
			{
				return Redirect(Request.Headers["Referer"].ToString());
			}
		}
    }
}