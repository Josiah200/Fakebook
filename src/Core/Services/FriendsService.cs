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
			return FixFriendsList(await _friendshipRepository.GetIncomingRequestsByUserIdAsync(userId), userId);
		}
		
		public async Task<bool> SendRequestAsync(User sender, User reciever)
		{
			var friendship = new Friendship 
			{
				UserId = sender.Id,
				FriendId = reciever.Id,
				Status = Status.Pending,
				Timestamp = DateTime.UtcNow
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
			return await _friendshipRepository.DeleteAsync(friendship);
		}

		public async Task<Friendship> GetFriendAsync(User user, User friend)
		{
			return await _friendshipRepository.GetFriendAsync(user, friend);
		}


		public async Task<List<Friendship>> GetFriendsListByUserIdAsync(string userId)
		{
			return FixFriendsList(await _friendshipRepository.GetByUserIdAsync(userId), userId);
		}

		/// <summary>
		/// Outputs a list of friendships with input user as Friendship.User
		/// </summary>
		private static List<Friendship> FixFriendsList(List<Friendship> list, string userId)
		{
			List<Friendship> fixedList = new();
			foreach (Friendship fr in list)
			{
				if (fr.FriendId == userId)
				{
					(fr.User, fr.Friend) = (fr.Friend, fr.User);
					fr.UserId = fr.User.Id;
					fr.FriendId = fr.Friend.Id;
					fixedList.Add(fr);
				}
				else
				{
					fixedList.Add(fr);
				}
			}
			return fixedList;
		}
	}
}