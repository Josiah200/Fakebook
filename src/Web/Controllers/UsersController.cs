using System.Threading.Tasks;
using Fakebook.Core.Interfaces;
using Fakebook.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Controllers
{
    public class UsersController : Controller
    {
		private readonly IUserService _userService;

		public UsersController(IUserService userService)
		{
			_userService = userService;
		}

		[Route("Users/{searchString:alpha}/{page:int?}")]
		[Route("Users/{page:int?}")]
    	public async Task<IActionResult> Index(string searchString, int page)
		{
			var viewModel = new UsersViewModel
			{
				Users = await _userService.GetChunkAsync(searchString, page),
				Page = page,
				SearchString = searchString
			};

			return View(viewModel);
		}
    }
}