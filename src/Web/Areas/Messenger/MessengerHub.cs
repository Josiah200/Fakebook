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
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMessengerService _messengerService;

		public MessengerHub(UserManager<ApplicationUser> userManager, IMessengerService messengerService)
		{
			_userManager = userManager;
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
					await Clients.Client(c.Id).SendAsync("SendSuccess");
				}
				await Clients.Client(connectionId).SendAsync("SendSuccess");
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