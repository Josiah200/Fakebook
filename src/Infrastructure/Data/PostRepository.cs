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
		
		public async Task<IReadOnlyList<Post>> GetHomePostsBlockAsync(List<string> friendIds, int page, int blocksize)
		{
			return await _dbContext.Posts
				.Include(p => p.User)
				.Where(p=> friendIds.Contains(p.UserId))
				.OrderByDescending(p => p.DatePosted)
				.Skip(page*blocksize)
				.Take(blocksize)
				.ToListAsync();
		}
		public async Task<IReadOnlyList<Post>> GetUserPostsBlockAsync(string userId, int page, int blocksize)
		{
			return await _dbContext.Posts
				.Include(p => p.User)
				.Where(p => p.UserId == userId)
				.OrderByDescending(p => p.DatePosted)
				.Skip(page*blocksize)
				.Take(blocksize)
				.ToListAsync();
		}
	}
}
