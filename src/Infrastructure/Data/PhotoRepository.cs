using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;

namespace Fakebook.Infrastructure.Data
{
	public class PhotoRepository : EfRepository<Photo>, IPhotoRepository
	{
        public PhotoRepository(FakebookContext dbContext) : base(dbContext)
		{
		}
    }
}