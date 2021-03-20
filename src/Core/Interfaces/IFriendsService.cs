using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IFriendsService
    {
        Task<List<Friendship>> GetByUserIdAsync(string userId);
		Task<List<Friendship>> GetIncomingRequestsByUserIdAsync(string userId);
		Task<bool> SendRequestAsync(User sender, User reciever);
		Task<Friendship> GetFriendAsync(User user, User friend);
		Task<bool> RemoveFriendAsync(User user, User friend);
		Task<bool> AcceptRequestAsync(User user, User sender);
	}
}