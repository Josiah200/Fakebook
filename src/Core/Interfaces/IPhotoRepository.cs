using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IPhotoRepository : IAsyncRepository<Photo>
    {
		Task<Photo> GetProfilePhotoAsync(string userId);
    }
}