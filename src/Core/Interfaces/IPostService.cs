using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPostService
    {
		Task<bool> SavePostAsync(Post post);
		Task<List<Post>> GetPostsBlockAsync(int page, int blocksize);
		Task<List<Post>> GetUserPostsBlockAsync(string userId, int page, int blocksize);
    }
}