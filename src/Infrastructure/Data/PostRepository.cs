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
		
		public async Task<IReadOnlyList<Post>> GetHomePostsBlockAsync(List<string> userIds, int page, int blockSize)
		{
			return await _dbContext.Posts
				.Include(p => p.User)
				.Where(p=> userIds.Contains(p.UserId))
				.OrderByDescending(p => p.DatePosted)
				.Skip(page*blockSize)
				.Take(blockSize)
				.ToListAsync();
		}
		public async Task<IReadOnlyList<Post>> GetUserPostsBlockByPublicIdAsync(string userPublicId, int page, int blockSize)
		{
			return await _dbContext.Posts
				.Include(p => p.User)
				.Where(p => p.User.PublicId == userPublicId)
				.OrderByDescending(p => p.DatePosted)
				.Skip(page*blockSize)
				.Take(blockSize)
				.ToListAsync();
		}
	}
}
