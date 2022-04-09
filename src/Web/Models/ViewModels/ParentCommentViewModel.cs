using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Web.Models.ViewModels
{
    public class ParentCommentViewModel : CommentViewModel
    {
		public List<CommentViewModel> Replies { get; set; }
    }
}