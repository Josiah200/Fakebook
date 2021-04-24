using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fakebook.Core.Interfaces;
using Fakebook.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
		
		[Authorize]
		[HttpGet("Users/{page:int}")]
    	public async Task<IActionResult> Index(string search, int page)
		{
			var users = await _userService.GetPageAsync(search, page - 1);
			var viewModel = new UsersViewModel
			{
				Users = users.Select(_mapper.Map<UserViewModel>),
				Page = page,
				SearchString = search
			};

			return View(viewModel);
		}
    }
}