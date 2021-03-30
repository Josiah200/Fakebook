using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPostRepository : IAsyncRepository<Post>
    {
		Task<IReadOnlyList<Post>?> GetHomePostsBlockAsync(List<string> friendIds, int page, int blockSize);
		Task<IReadOnlyList<Post>?> GetUserPostsBlockAsync(string userId, int page, int blockSize);
    }
}