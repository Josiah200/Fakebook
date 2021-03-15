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
				.Where(f => f.UserId == userId ^ f.FriendId == userId)
				.ToListAsync();
		}

		public Task<List<Friendship>> GetIncomingRequestsByUserIdAsync(string userId)
		{
			return _dbContext.Friendships
				.Include(f => f.User)
				.Include(f => f.Friend)
				.Where(f => f.FriendId == userId && f.Status == 0)
				.OrderByDescending(f => f.Timestamp)
				.ToListAsync();
		}

		public Task<int> GetNumRequestsByUserIdAsync(string userId)
		{
			return _dbContext.Friendships
				.Where(f => f.FriendId == userId && f.Status == 0)
				.CountAsync();
		}
		public Task<Friendship> GetFriendAsync(User user, User friend)
		{
			return _dbContext.Friendships
				.Where(f => f.UserId == user.Id ^ f.UserId == friend.Id)
				.Where(f => f.FriendId == user.Id ^ f.FriendId == friend.Id)
				.SingleOrDefaultAsync();
		}
	}
}