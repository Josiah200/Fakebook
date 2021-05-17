using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface ILikeService<T> where T : BaseEntity
    {
        Task<bool> LikeAsync(string postId, string userId);
		Task<bool> UnlikeAsync(string postId, string userId);
		Task<bool> CheckIfUserLikesAsync(string postId, string userId);
    }
}