using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Models.ViewModels
{
    public class NewPhotoModel
    {
		[Required(ErrorMessage = "Please select an Image.")]
		[DataType(DataType.Upload)]
        public IFormFile File { get; set; }
		public bool IsProfilePicture { get; set; }
		public string UserId { get; set; }
    }
}