using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fakebook.Infrastructure.Data
{
	public class FriendshipRepository : EfRepository<Friendship>, IFriendshipRepository
	{
		public FriendshipRepository(FakebookContext dbContext) : base(dbContext)
		{
		}

		public Task<List<Friendship>> GetByUserIdAsync(string userId)
		{
			return _dbContext.Friendships
				.Where(f => (f.UserId == userId || f.FriendId == userId) && f.Status.Equals(Status.Accepted))
				.Include(f => f.User)
				.Include(f => f.Friend)
				.ToListAsync();
		}

		public Task<List<Friendship>> GetIncomingRequestsByUserIdAsync(string userId)
		{
			return _dbContext.Friendships
				.Where(f => f.FriendId == userId && f.Status.Equals(Status.Pending))
				.Include(f => f.User)
				.Include(f => f.Friend)
				.OrderByDescending(f => f.Timestamp)
				.ToListAsync();
		}

		public Task<Friendship> GetFriendAsync(User user, User friend)
		{
			return _dbContext.Friendships
				.Where(f => (f.UserId == user.Id && f.FriendId == friend.Id) || (f.UserId == friend.Id && f.FriendId == user.Id))
				.FirstOrDefaultAsync();
		}

		public async Task<bool> AcceptRequestAsync(Friendship friendship)
		{
			friendship.Status = Status.Accepted;
			var acceptResult = await _dbContext.SaveChangesAsync();
			return acceptResult == 1;
		}

		public Task<List<Friendship>> GetWithMessagesAsync(string userId)
		{
			return _dbContext.Friendships
			.Where(f => (f.UserId == userId || f.FriendId == userId) && f.Status.Equals(Status.Accepted))
			.Include(f => f.User)
			.Include(f => f.Friend)
			.Include(f => f.Messages.OrderByDescending(m => m.TimeStamp))
			.ToListAsync();
		}
	}
}