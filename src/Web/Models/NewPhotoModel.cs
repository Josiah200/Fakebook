using System.ComponentModel.DataAnnotations;
using Fakebook.Web.Attributes.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Models
{
    public class NewPhotoModel
    {
		[Required(ErrorMessage = "Please select an image.")]
		[DataType(DataType.Upload)]
		[MaxFileSize(1048576, ErrorMessage = "Image too large. Maximum file size is 1MB")]
		[AllowedFileExtensions(new string[] { ".jpg", ".png", ".jpeg" }, ErrorMessage = "Invalid file type")]
        public IFormFile File { get; set; }
    }
}