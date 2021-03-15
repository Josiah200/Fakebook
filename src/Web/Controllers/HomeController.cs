using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Fakebook.Web.ViewModels;

namespace Fakebook.Web.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly INotificationsService _notificationsService;
		private readonly IFriendsService _friendsService;
		private readonly IPostService _postService;
		public HomeController(
			UserManager<ApplicationUser> userManager,
			INotificationsService notificationsService,
			IFriendsService friendsService,
			IPostService postService
			)
		{
			_userManager = userManager;
			_notificationsService = notificationsService;
			_friendsService = friendsService;
			_postService = postService;
		}

    	public async Task<IActionResult> Index()
		{
			var user = await _userManager.GetUserAsync(User);
			var viewModel = new HomeViewModel
			{
				CurrentUser = user,
				NotificationsNum = 2,
				RequestsNum = await _friendsService.GetNumRequestsByUserIdAsync(user.Id)
			};

			return View(viewModel);
		}

    }
}
