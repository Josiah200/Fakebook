using System.Threading.Tasks;
using Fakebook.Core.Interfaces;
using Fakebook.Core.Services;
using Fakebook.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Controllers
{
	[Route("[Controller]")]
	[ApiController]
    public class NotificationsController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly INotificationsService _notificationsService;
		public NotificationsController(UserManager<ApplicationUser> userManager, INotificationsService notificationsService)
		{
			_userManager = userManager;
			_notificationsService = notificationsService;
		}

		[HttpGet]
		[Authorize]
		public async Task<ActionResult> GetNotifications()
		{
			var currentUser = await _userManager.GetUserAsync(User);
			if (currentUser == null) return Challenge();

			var notifications = await _notificationsService.GetByUserIdAsync(currentUser.Id);
			return PartialView("_NotificationsPartial", notifications);
		}
	}
}
