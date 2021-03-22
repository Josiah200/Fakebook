using System.Threading.Tasks;
using Fakebook.Core.Interfaces;
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
			var viewModel = await _userService.GetChunkAsync(searchString, page);
			return View(viewModel);
		}
    }
}