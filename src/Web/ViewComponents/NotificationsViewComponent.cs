using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.ViewComponents
{
	public class NotificationsViewComponent : ViewComponent
	{
        private readonly INotificationsService _notificationsService;
		private readonly UserManager<ApplicationUser> _userManager;

		public NotificationsViewComponent(INotificationsService notificationsService, UserManager<ApplicationUser> userManager)
		{
			_notificationsService = notificationsService;
			_userManager = userManager;
		}

		[Authorize]
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var currentUser = await _userManager.GetUserAsync((ClaimsPrincipal)User);
			var notifications = await _notificationsService.GetByUserIdAsync(currentUser.Id);
			return View(notifications);	
		}
    }
}