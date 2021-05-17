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
		
		public async Task<IReadOnlyList<Post>> GetPostsPageByUserIdListAsync(List<string> userIds, int page, int pageSize)
		{
			return await _dbContext.Posts
				.Include(p => p.User)
				.Include(p => p.Likes)
				.Include(p => p.Comments.OrderByDescending(c => c.DatePosted))
				.ThenInclude(c => c.User)
				.Include(p => p.Comments.OrderByDescending(c => c.DatePosted))
				.ThenInclude(c => c.Likes)
				.Where(p=> userIds.Contains(p.UserId))
				.OrderByDescending(p => p.DatePosted)
				.Skip(page*pageSize)
				.Take(pageSize)
				.ToListAsync();
		}
	}
}
