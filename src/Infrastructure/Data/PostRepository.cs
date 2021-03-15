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
		
		public Task<List<Post>> GetPostsBlockAsync(int page, int blocksize)
		{
			return _dbContext.Posts
				.Include(p => p.User)
				.OrderByDescending(p => p.DatePosted)
				.Skip(page*blocksize)
				.Take(blocksize)
				.ToListAsync();
		}
		public Task<List<Post>> GetUserPostsBlockAsync(string userId, int page, int blocksize)
		{
			return _dbContext.Posts
				.Where(p => p.UserId == userId)
				.OrderByDescending(p => p.DatePosted)
				.Skip(page*blocksize)
				.Take(blocksize)
				.ToListAsync();
		}
	}
}
