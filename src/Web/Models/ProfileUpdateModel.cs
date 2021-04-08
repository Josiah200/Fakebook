using System;
using System.ComponentModel.DataAnnotations;

namespace Fakebook.Web.Models
{
	public class ProfileUpdateModel
	{
		[DataType(DataType.Text)]
		[Display(Name = "First name")]
		public string FirstName { get; set; }
		[Required]
		[DataType(DataType.Text)]
		[Display(Name = "Last name")]
		public string LastName { get; set; }
		[DataType(DataType.Text)]
		[Display(Name = "Gender")]
		public string? Gender { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "Birthdate")]
		public DateTime? Birthdate { get; set; }
	}
}