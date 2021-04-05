using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fakebook.Core.Interfaces;
using Fakebook.Web.Models;
using Fakebook.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Controllers
{
    public class UsersController : Controller
    {
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public UsersController(IUserService userService, IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
		}

		[Route("Users/{searchString:alpha}/{page:int?}")]
		[Route("Users/{page:int?}")]
    	public async Task<IActionResult> Index(string searchString, int page)
		{
			var users = await _userService.GetPageAsync(searchString, page);
			var viewModel = new UsersViewModel
			{
				Users = users.Select(_mapper.Map<UserModel>),
				Page = page,
				SearchString = searchString
			};

			return View(viewModel);
		}
    }
}