using System.ComponentModel.DataAnnotations;
using Fakebook.Web.Attributes.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fakebook.Web.Models.ViewModels
{
	public class NewCommentModel
	{
		[Required(ErrorMessage = "Write a comment")]
		[DataType(DataType.Text)]
		public string Text { get; set; }
		[Required]
		public string PostId { get; set; }
		public string CommentId { get; set; }
	}
}