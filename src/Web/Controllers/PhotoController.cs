using System.Threading.Tasks;
using Fakebook.Core.Interfaces;
using Fakebook.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Controllers
{
    public class PhotoController : ControllerBase
    {
		private readonly IPhotoService _photoService;
		private readonly UserManager<ApplicationUser> _userManager;

		public PhotoController(IPhotoService photoService, UserManager<ApplicationUser> userManager)
		{
			_photoService = photoService;
			_userManager = userManager;
		}

		[HttpPost]
		public async Task<IActionResult> NewPhoto(IFormFile image)
		{
			var currentApplicationUser = await _userManager.GetUserAsync(User);
			var contentType = image.ContentType;
			
			var result = await _photoService.NewPhotoAsync(image, currentApplicationUser.Id);
			return Ok();
		}
    }
}