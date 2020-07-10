using System.Linq;
using FakeBook.Core.Entities;
using FakeBook.Core.Interfaces;

namespace FakeBook.Infrastructure.Data
{
	public class PostRepository : EfRepository<Post>, IPostRepository
	{
		public PostRepository(FakeBookContext dbContext) : base(dbContext)
		{
		}
		public IQueryable<Post> Posts => throw new System.NotImplementedException();
	}
}