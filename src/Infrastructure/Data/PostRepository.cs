using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fakebook.Infrastructure.Data
{
	public class PostRepository : EfRepository<Post>, IPostRepository
	{
		public PostRepository(FakebookContext dbContext) : base(dbContext)
		{
		}
		
		public Task<List<Post>> GetHomePostsAsync()
		{
			return _dbContext.Posts
				.Include(p => p.User)
				.ToListAsync();
		}
		public Task<List<Post>> GetUserPostsAsync(string userId)
		{
			return _dbContext.Posts
				.Where(p => p.UserId == userId)
				.ToListAsync();
		}
	}
}
