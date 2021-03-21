using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPostService
    {
		Task<bool> SavePostAsync(Post post);
		Task<List<Post>>? GetFriendsPostsBlockAsync(int page, int blocksize, string userId);
		Task<List<Post>>? GetUserPostsBlockAsync(int page, int blocksize, string userId);
    }
}