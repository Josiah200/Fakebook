using System.Collections.Generic;
using Fakebook.Core.Entities;

namespace Fakebook.Web.ViewModels
{
    public class UsersViewModel
    {
        public List<User> Users { get; set; }
		public int Page { get; set; }
		public string SearchString { get; set; }
    }
}