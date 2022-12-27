using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Fakebook.Core.Interfaces;
using Fakebook.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Controllers
{
    public class UserSearchController : Controller
    {
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public UserSearchController(IUserService userService, IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
		}
		
		[Authorize]
		[HttpGet,Route("UserSearch/{q?}/{page?}")]
    	public async Task<IActionResult> Index(string q, int page)
		{
			var users = await _userService.GetPageAsync(q, page - 1);
			var viewModel = new UserSearchViewModel
			{
				Users = users.Select(_mapper.Map<UserViewModel>),
				Page = page,
				SearchString = q
			};

			return View(viewModel);
		}
    }
}