using System.Threading.Tasks;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Fakebook.Web.Attributes.ValidationAttributes;
using Fakebook.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Controllers
{
	[Route("[Controller]")]
	[ApiController]
    public class PhotoController : ControllerBase
    {
		private readonly IPhotoService _photoService;
		private readonly IUserService _userService;
		private readonly UserManager<ApplicationUser> _userManager;

		public PhotoController(IPhotoService photoService, IUserService userService, UserManager<ApplicationUser> userManager)
		{
			_photoService = photoService;
			_userService = userService;
			_userManager = userManager;
		}

        [HttpPost]
		[Route("NewProfilePhoto")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> NewProfilePhoto([FromForm] NewPhotoModel photoInput)
		{
			var currentApplicationUser = await _userManager.GetUserAsync(User);
			var currentUser = await _userService.GetByIdAsync(currentApplicationUser.Id);
			var successful = await _photoService.NewProfilePictureAsync(photoInput.File, currentUser);
			if (successful)
			{
				return Content($"<h5>Updated</h5>");
			}
			else
			{
				return Content($"<h5>Internal error, please reload and try again<h5>");
			}
		}
    }
}