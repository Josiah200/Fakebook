using System.Collections.Generic;
using Fakebook.Core.Entities;
using Fakebook.Infrastructure.Identity;

namespace Fakebook.Web.ViewModels
{
    public class FriendsViewModel
    {
		public List<Friendship> Friends { get; set; }
		public List<Friendship> Requests { get; set; }
    }
}