using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Fakebook.Web.Models.ViewModels;

namespace Fakebook.Web.Controllers
{
	[Authorize]
    public class HomeController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		
		public HomeController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

    	public async Task<IActionResult> Index()
		{
			var viewModel = new HomeViewModel();
			
			if(User != null)
			{
				var user = await _userManager.GetUserAsync(User);
				viewModel.CurrentUserId = user.Id;
			}

			return View(viewModel);
		}
    }
}
