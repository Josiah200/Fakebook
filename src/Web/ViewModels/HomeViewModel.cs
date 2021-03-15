using System.Collections.Generic;
using Fakebook.Core.Entities;
using Fakebook.Infrastructure.Identity;

namespace Fakebook.Web.ViewModels
{
    public class HomeViewModel
	{
		public ApplicationUser CurrentUser { get; set; }
		public int RequestsNum { get; set; }
		public int NotificationsNum { get; set; }
	}
}