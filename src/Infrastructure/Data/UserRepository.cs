using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fakebook.Infrastructure.Data
{
	public class UserRepository : EfRepository<User>, IUserRepository
	{
		public UserRepository(FakebookContext dbContext) : base(dbContext)
		{
		}

		public Task<User> GetByPublicIdAsync(string userPublicId)
		{
			return _dbContext.Users
				.FirstOrDefaultAsync(x => x.PublicId == userPublicId);
		}

		public Task<List<User>> GetChunkAsync(int page)
		{
			return _dbContext.Users
				.OrderBy(u => u.FirstName)
				.ThenBy(u => u.LastName)
				.Skip(page * 100)
				.Take(100)
				.ToListAsync();
		}
	}
}