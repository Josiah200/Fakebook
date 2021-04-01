using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPostService
    {
		Task<bool> SavePostAsync(Post post);
		Task<IReadOnlyCollection<Post>?> GetHomePostsBlockAsync(int page, int blocksize, string userId);
		Task<IReadOnlyCollection<Post>?> GetUserPostsBlockAsync(int page, int blocksize, string userPublicId);
    }
}