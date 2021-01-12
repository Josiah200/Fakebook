using System.Collections.Generic;
using Fakebook.Core.Entities;

namespace Fakebook.Web.ViewModels
{
    public class InfiniteScrollViewModel
    {
        public IReadOnlyList<Post> Posts { get; set; }
    }
}