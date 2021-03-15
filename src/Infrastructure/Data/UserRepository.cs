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

		public async Task<User> GetByIdAsync(string id)
		{
			return await _dbContext.Users
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public Task<User> GetByPublicIdAsync(string userPublicId)
		{
			return _dbContext.Users
				.FirstOrDefaultAsync(x => x.PublicId == userPublicId);
		}
	}
}