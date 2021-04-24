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

		public Task<List<User>> GetPageAsync(string searchString, int page)
		{
			return _dbContext.Users
				.Where(u => u.FirstName.ToLower().Contains(searchString) |
					u.LastName.ToLower().Contains(searchString) |
					(u.FirstName.ToLower() + " " + u.LastName.ToLower()).Contains(searchString))
				.OrderBy(u => u.FirstName)
				.ThenBy(u => u.LastName)
				.Skip(page * 40)
				.Take(40)
				.ToListAsync();
		}
	}
}