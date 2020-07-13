using System.Linq;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;

namespace Fakebook.Infrastructure.Data
{
	public class PostRepository : EfRepository<Post>, IPostRepository
	{
		public PostRepository(FakebookContext dbContext) : base(dbContext)
		{
		}
		public IQueryable<Post> Posts => throw new System.NotImplementedException();
	}
}