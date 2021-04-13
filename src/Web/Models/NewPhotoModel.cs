using System.ComponentModel.DataAnnotations;
using Fakebook.Web.Attributes.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Models.ViewModels
{
    public class NewPhotoModel
    {
		[Required(ErrorMessage = "Please select an Image.")]
		[DataType(DataType.Upload)]
		[MaxFileSize(2097152)]
		[AllowedFileExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public IFormFile File { get; set; }
    }
}