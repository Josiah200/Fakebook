using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPostService
    {
		Task<bool> SavePostAsync(Post post);
		Task<Post> GetByIdAsync(string postId);
		Task<IReadOnlyCollection<Post>?> GetHomePostsPageAsync(string userId, int page, int pageSize, List<Friendship>? friends);
		Task<IReadOnlyCollection<Post>?> GetUserPostsPageAsync(string userId, int page, int pageSize);
	}
}