using System.Collections.Generic;
using Fakebook.Core.Entities;
using Fakebook.Infrastructure.Identity;

namespace Fakebook.Web.Models.ViewModels
{
    public class HomeViewModel
	{
		public ApplicationUser CurrentUser { get; set; }
	}
}