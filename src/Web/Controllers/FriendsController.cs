using System;
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
using Fakebook.Web.Models.ViewModels;

namespace Fakebook.Web.Controllers
{
	[Route("[Controller]")]
	public class FriendsController : Controller
    {
		private readonly IFriendsService _friendsService; 
		private readonly IUserService _userService;
		private readonly IMessengerService _messengerService;
		private readonly UserManager<ApplicationUser> _userManager;

	    public FriendsController(IFriendsService friendsService, IUserService userService, IMessengerService messengerService,UserManager<ApplicationUser> userManager)
		{
			_friendsService = friendsService;
			_userService = userService;
			_messengerService = messengerService;
			_userManager = userManager;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetFriends()
		{
			var currentUser = await _userManager.GetUserAsync(User);
			var friends = await _friendsService.GetFriendsListByUserIdAsync(currentUser.Id);
			if (friends.Count == 0)
			{
				return NotFound();
			}
			return PartialView("_FriendsChunkPartial", friends);
		}

		[HttpGet("Requests")]
		[Authorize]
		public async Task<IActionResult> GetFriendRequests()
		{
			var currentUser = await _userManager.GetUserAsync(User);
			var requests = await _friendsService.GetIncomingRequestsByUserIdAsync(currentUser.Id);
			if (requests is null)
			{
				return NotFound();
			}
			return PartialView("_FriendsChunkPartial", requests);
		}
		
		[Route("Add/{userPublicId}")]
		[Authorize]
		public async Task<IActionResult> AddFriend(string userPublicId)
		{
			var currentApplicationUser = await _userManager.GetUserAsync(User);

			if (currentApplicationUser is null)
			{
				return Challenge();
			}

			var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);

			var reciever = await _userService.GetByPublicIdAsync(userPublicId);

			if (currentUser == reciever || currentUser is null || reciever is null)
			{
				return BadRequest();
			}

			var friendship = await _friendsService.GetFriendAsync(currentUser, reciever);

			if (friendship is null)
			{
				await _friendsService.SendRequestAsync(currentUser, reciever);
			}

			if (friendship != null && friendship.FriendId == currentUser.Id)
			{
				await _friendsService.AcceptRequestAsync(currentUser, reciever);
			}
			
			return Redirect(Request.Headers["Referer"].ToString());
		}

		[Route("Remove/{userPublicId}")]
		[Authorize]
		public async Task<IActionResult> RemoveFriend(string userPublicId)
		{
			var currentApplicationUser = await _userManager.GetUserAsync(User);

			if (currentApplicationUser == null)
			{
				return Challenge();
			}

			var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);
			var friend = await _userService.GetByPublicIdAsync(userPublicId);

			if (currentUser == null || friend == null)
			{
				return BadRequest();
			}

			await _friendsService.RemoveFriendAsync(currentUser, friend);
			return Redirect(Request.Headers["Referer"].ToString());
		}

		[HttpGet("Messenger")]
		[Authorize]
		public async Task<IActionResult> Messenger()
		{
			var currentUser = await _userManager.GetUserAsync(User);
			var friends = await _friendsService.GetFriendsWithMessagesAsync(currentUser.Id);

			if (friends.Count == 0)
			{
				return NotFound();
			}
			var viewModel = new MessengerViewModel()
			{
				Friends = friends,
				Messages = new List<List<Message>>()
			};

			foreach(Friendship friendship in friends)
			{
				viewModel.Messages.Add(await _messengerService.GetAllMessagesAsync(currentUser.Id, friendship.FriendId));
			}
			
			return PartialView("_MessengerFriendsPartial", viewModel);
		}
	}
}