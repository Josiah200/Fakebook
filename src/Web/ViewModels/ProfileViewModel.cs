using System.Collections.Generic;
using Fakebook.Core.Entities;

namespace Fakebook.Web.ViewModels
{
    public class ProfileViewModel
    {
        public IReadOnlyList<Post> Posts { get; set; }
    }
}