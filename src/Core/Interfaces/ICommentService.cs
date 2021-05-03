using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface ICommentService
    {
        Task<bool> SaveCommentAsync(Comment comment);
		Task<bool> LikeCommentAsync(string commentId, string userId);
		Task<bool> UnlikeCommentAsync(string commentId, string userId);
		Task<bool> CheckIfUserLikesCommentAsync(string commentId, string userId);
    }
}