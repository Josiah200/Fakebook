using System.Collections.Generic;
using Fakebook.Core.Entities;

namespace Fakebook.Web.Models.ViewModels
{
    public class UsersViewModel
    {
        public IEnumerable<UserModel> Users { get; set; }
		public int Page { get; set; }
		public string SearchString { get; set; }
    }
}