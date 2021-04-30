using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPostService
    {
		Task<bool> SavePostAsync(Post post);
		Task<IReadOnlyCollection<Post>?> GetHomePostsPageAsync(string userId, int page, int pageSize);
		Task<IReadOnlyCollection<Post>?> GetUserPostsPageAsync(string userId, int page, int pageSize);
		Task<bool> LikePostAsync(string postId, string userId);
		Task<bool> CheckIfUserLikes(string postId, string id);
		Task<bool> UnlikePostAsync(string postId, string id);
	}
}