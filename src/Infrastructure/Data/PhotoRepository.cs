using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fakebook.Infrastructure.Data
{
	public class PhotoRepository : EfRepository<Photo>, IPhotoRepository
	{
        public PhotoRepository(FakebookContext dbContext) : base(dbContext)
		{
		}

		public async Task<Photo> GetProfilePhotoAsync(string userId)
		{
			return await _dbContext.Photos
			.Where(p => p.UserId == userId)
			.Where(p => p.IsProfilePicture == true)
			.FirstOrDefaultAsync();
		}
    }
}