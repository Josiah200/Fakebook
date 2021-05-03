using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;

namespace Fakebook.Infrastructure.Data
{
    public class CommentRepository : EfRepository<Comment>, ICommentRepository
    {
		public CommentRepository(FakebookContext dbContext) : base(dbContext)
		{
		}
    }
}