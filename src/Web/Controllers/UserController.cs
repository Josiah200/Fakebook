using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Fakebook.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Controllers
{
	[ApiController]
	[Route("[Controller]")]
    public class UserController : ControllerBase
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IUserService _userService;
		private readonly IMapper _mapper;

		public UserController(IUserService userService, UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_userService = userService;
			_userManager = userManager;
			_mapper = mapper;
		}
		
		[HttpPost]
		[Route("UpdateProfile")]
		public async Task<IActionResult> UpdateProfile([FromForm] UserProfileUpdateModel updateInput)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var currentApplicationUser = await _userManager.GetUserAsync(User);
			var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);
			User userInput = _mapper.Map<User>(updateInput);

			userInput.Bio = HttpUtility.HtmlEncode(userInput.Bio);

			var successful = await _userService.UpdateProfileAsync(currentUser, userInput);

			if (successful)
			{
				return Content($"<h5>Updated.</h5>");
			}
			else
			{
				return Content($"<h5>Internal error, please reload and try again.<h5>");
			}
		}

		[HttpPost]
		[Route("UpdatePublicId")]
		public async Task<IActionResult> UpdatePublicId([FromForm] NewPublicIdModel publicIdInput)
		{
			if (publicIdInput.PublicId.All(x => char.IsLetterOrDigit(x)))
			{
				return Content($"<h5>Only Letters and numbers allowed in Public Id.</h5>");
			}

			if (await _userService.GetByPublicIdAsync(publicIdInput.PublicId) != null)
			{
				return Content($"<h5>Public ID Taken.</h5>");
			}

			var currentApplicationUser = await _userManager.GetUserAsync(User);
			var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);

			bool successful = await _userService.UpdatePublicIdAsync(currentUser, publicIdInput.PublicId);

			if (successful)
			{
				return Content($"<h5>Updated.</h5>");
			}
			else
			{
				return Content($"<h5>Internal error, please reload and try again.<h5>");
			}
		}
    }
}