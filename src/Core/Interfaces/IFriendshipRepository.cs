using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IFriendshipRepository : IAsyncRepository<Friendship>
    {
		Task<List<Friendship>> GetByUserIdAsync(string userId);
		Task<List<Friendship>> GetIncomingRequestsByUserIdAsync(string userId);
		Task<int> GetNumRequestsByUserIdAsync(string userId);
		Task<Friendship> GetFriendAsync(User user, User friend);
	}
}