using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Fakebook.Web.Attributes.ValidationAttributes
{
    public class AllowedFileExtensionsAttribute : ValidationAttribute, IClientModelValidator
	{
        private readonly string[] _extensions;

		public AllowedFileExtensionsAttribute(string[] extensions)
		{
			_extensions = extensions;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is IFormFile file)
			{
				var extension = Path.GetExtension(file.FileName);
				
				if (!_extensions.Contains(extension.ToLower()))
				{
					return new ValidationResult(ErrorMessage);
				}
			}
			return ValidationResult.Success;
		}

		public void AddValidation(ClientModelValidationContext context)
		{
			MergeAttribute(context.Attributes, "data-val", "true");
			MergeAttribute(context.Attributes, "data-val-allowedfileextensions", ErrorMessage);
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
