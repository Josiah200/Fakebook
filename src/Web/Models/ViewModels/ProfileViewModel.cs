using System.Collections.Generic;
using Fakebook.Core.Entities;

namespace Fakebook.Web.Models.ViewModels
{
    public class ProfileViewModel
    {
		public User ProfileUser { get; set; }
		public Friendship Friendship { get; set; }
    }
}