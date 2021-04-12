using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Fakebook.Core.Interfaces
{
    public interface IPhotoService
    {
        Task<bool> NewPhotoAsync(IFormFile image, string userId, bool IsProfilePicture);
	}
}