using Fakebook.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Fakebook.Web.Models.ViewModels
{
    public class ProfileViewModel
    {
		public User ProfileUser { get; set; }
		public bool IsProfileOwner { get; set; }
		public Friendship? Friendship { get; set; }
		public User UpdateInput { get; set; }
		public IFormFile Image { get; set; }
    }
}