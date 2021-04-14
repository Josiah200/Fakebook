using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Fakebook.Web.Attributes.ValidationAttributes
{
    public class MaxFileSizeAttribute : ValidationAttribute, IClientModelValidator
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
					return new ValidationResult(ErrorMessage);
				}
			}
			return ValidationResult.Success;
		}

		public void AddValidation(ClientModelValidationContext context)
		{
			MergeAttribute(context.Attributes, "data-val", "true");
			MergeAttribute(context.Attributes, "data-val-maxfilesize", ErrorMessage);
		}

		private static bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
    	{
			if (attributes.ContainsKey(key))
			{
				return false;
			}

			attributes.Add(key, value);
			return true;
    	}
    }
}
