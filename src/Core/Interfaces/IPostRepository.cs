using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPostRepository : IAsyncRepository<Post>
    {
		Task<List<Post>> GetHomePostsAsync(int page);
		Task<List<Post>> GetUserPostsAsync(string userId);
    }
}