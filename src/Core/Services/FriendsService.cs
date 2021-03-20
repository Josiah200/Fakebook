using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;

namespace Fakebook.Core.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly IFriendshipRepository _friendshipRepository;
		public FriendsService(IFriendshipRepository friendshipRepository)
		{
			_friendshipRepository = friendshipRepository;
		}

		public async Task<List<Friendship>> GetByUserIdAsync(string userId)
		{
			return await _friendshipRepository.GetByUserIdAsync(userId);
		}
		
		public async Task<List<Friendship>> GetIncomingRequestsByUserIdAsync(string userId)
		{
			return await _friendshipRepository.GetIncomingRequestsByUserIdAsync(userId);
		}
		
		public async Task<bool> SendRequestAsync(User sender, User reciever)
		{
			var friendship = new Friendship 
			{
				UserId = sender.Id,
				FriendId = reciever.Id,
				Status = Status.Pending,
				Timestamp = DateTime.Now
			};
			return await _friendshipRepository.AddAsync(friendship);
		}

		public async Task<bool> AcceptRequestAsync(User user, User sender)
		{
			var friendship = await _friendshipRepository.GetFriendAsync(user, sender);
			return await _friendshipRepository.AcceptRequestAsync(friendship);
		}

		public async Task<bool> RemoveFriendAsync(User user, User friend)
		{
			var friendship = await _friendshipRepository.GetFriendAsync(user, friend);
			bool successful = await _friendshipRepository.DeleteAsync(friendship);
			return successful;
		}

		public async Task<Friendship> GetFriendAsync(User user, User friend)
		{
			try
			{
				return await _friendshipRepository.GetFriendAsync(user, friend);
			}
			catch (Exception ex)
			{
				return null;
			}
		}

	}
}