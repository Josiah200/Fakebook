using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface ICommentService
    {
        Task<bool> SaveCommentAsync(Comment comment);
		Task<Comment> GetByIdAsync(string commentId);
    }
}