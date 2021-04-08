using Fakebook.Core.Entities;

namespace Fakebook.Web.Models.ViewModels
{
    public class ProfileViewModel
    {
		public User ProfileUser { get; set; }
		public bool IsProfileOwner { get; set; }
		public Friendship? Friendship { get; set; }
		public User UpdateInput { get; set; }
    }
}