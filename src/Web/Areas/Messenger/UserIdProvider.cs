// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Fakebook.Core.Interfaces;
// using Microsoft.AspNetCore.SignalR;

// namespace Fakebook.Web.Areas.Messenger
// {
//     public class UserIdProvider : IUserIdProvider
// 	{
// 		private readonly IUserService _userService;

// 		public UserIdProvider(IUserService userService)
// 		{
// 			_userService = userService;
// 		}

// 		public string? GetUserId(HubConnectionContext connection)
// 		{
// 			return (_userService.GetByIdAsync(connection)).ToString();
// 		}
// 	}
// }