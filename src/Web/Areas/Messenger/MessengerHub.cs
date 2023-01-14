using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Fakebook.Web.Areas.Messenger
{
	[Authorize]
    public class MessengerHub : Hub
    {
		private readonly IMessengerService _messengerService;
		private readonly IUserService _userService;

		private readonly UserManager<ApplicationUser> _userManager;

		public MessengerHub(UserManager<ApplicationUser> userManager,
			IUserService userService, IMessengerService messengerService)
		{
			_userManager = userManager;
			_userService = userService;
			_messengerService = messengerService;
		}

		public async Task SendMessage(string targetId, string message)
		{
			string userName = Context.User.Identity.Name;
			string userId = _userManager.Users.FirstOrDefault(u => u.Email == userName).Id;

			string connectionId = Context.ConnectionId;
			List<Connection> connections = await _messengerService.GetConnectionsAsync(targetId);
			if (connections.Count == 0)
			{
				await Clients.Client(connectionId).SendAsync("SendFailed");
			}
			else
			{
				foreach (var c in connections)
				{
					await Clients.Client(c.Id).SendAsync("RecieveMessage", userId, message);
				}
				await Clients.Client(connectionId).SendAsync("SendSuccess");

				await _messengerService.SaveMessageAsync(new Message
				{
					Id = Guid.NewGuid().ToString(),
					SenderId = userId,
					RecieverId = targetId,
					Content = message,
					TimeStamp = DateTime.UtcNow
				});
			}
		}

		public override Task OnConnectedAsync()
		{
			string userName = Context.User.Identity.Name;
			string userId = _userManager.Users.FirstOrDefault(u => u.Email == userName).Id;
			string connectionId = Context.ConnectionId;

			_messengerService.SaveConnectionAsync(userId, connectionId);

			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception exception)
		{
			string userName = Context.User.Identity.Name;
			string userId = _userManager.Users.FirstOrDefault(u => u.Email == userName).Id;
			string connectionId = Context.ConnectionId;

			_messengerService.DeleteConnectionAsync(connectionId);
			
			return base.OnDisconnectedAsync(exception);
		}
    }
}