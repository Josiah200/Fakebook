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

		[Route("Users/{page?}")]
    	public async Task<IActionResult> Index(int? page)
		{
			var viewModel = await _userService.GetChunkAsync(page ?? 0);
			return View(viewModel);
		}
    }
}