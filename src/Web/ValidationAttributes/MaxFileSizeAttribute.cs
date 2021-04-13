using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Fakebook.Web.ValidationAttributes
{
	public class MaxFileSizeAttribute : ValidationAttribute
	{
        private readonly int _maxFileSize;

		public MaxFileSizeAttribute(int maxFileSize)
		{
			_maxFileSize = maxFileSize;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is IFormFile file)
			{
				if (file.Length > _maxFileSize)
				{
					return new ValidationResult($"Maximum file size is { _maxFileSize / 1048576 }MB");
				}
			}
			return ValidationResult.Success;
		}
    }
}