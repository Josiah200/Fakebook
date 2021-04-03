using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPostRepository : IAsyncRepository<Post>
    {
		Task<IReadOnlyList<Post>> GetPostsPageByUserIdListAsync(List<string> userIds, int page, int blockSize);
    }
}