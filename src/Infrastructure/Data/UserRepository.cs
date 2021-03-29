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
				.FirstOrDefaultAsync(u => u.PublicId == userPublicId);
		}

		public Task<List<User>> GetChunkAsync(string searchString, int page)
		{
			return _dbContext.Users
				.Where(u => u.FirstName.Contains(searchString) | u.LastName.Contains(searchString) | (u.FirstName + " " + u.LastName).Contains(searchString))
				.OrderBy(u => u.FirstName)
				.ThenBy(u => u.LastName)
				.Skip(page * 100)
				.Take(100)
				.ToListAsync();
		}
	}
}