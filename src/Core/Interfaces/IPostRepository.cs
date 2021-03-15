using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPostRepository : IAsyncRepository<Post>
    {
		Task<List<Post>> GetPostsBlockAsync(int page, int blocksize);
		Task<List<Post>> GetUserPostsBlockAsync(string userId, int page, int blocksize);
    }
}