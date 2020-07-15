using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;

namespace Fakebook.Infrastructure.Data
{
	public class PostRepository : EfRepository<Post>, IPostRepository
	{
		public PostRepository(FakebookContext dbContext) : base(dbContext)
		{
		}
	}
}