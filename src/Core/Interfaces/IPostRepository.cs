using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPostRepository : IAsyncRepository<Post>
    {
		Task<List<Post>> GetFriendsPostsBlockAsync(List<string> friendIds, int page, int blockSize);
		Task<List<Post>> GetUserPostsBlockAsync(string userId, int page, int blockSize);
    }
}