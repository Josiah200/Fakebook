// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Fakebook.Web.Areas.Messenger;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.SignalR;

// namespace Fakebook.Web.Controllers
// {
// 	[Route("Messenger")]
// 	[ApiController]
//     public class MessengerController : ControllerBase
//     {
//         private readonly IHubContext<MessengerHub> _hubContext;

// 		public MessengerController(IHubContext<MessengerHub> hubContext)
// 		{
// 			_hubContext = hubContext;
// 		}

// 		[Route("send")]
// 		[HttpPost]
// 		public IActionResult SendMessage([FromBody] MessageDto msg)
// 		{
// 			_hubContext.Clients.All.SendAsync();
// 		}
//     }
// }