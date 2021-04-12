using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace Fakebook.Core.Interfaces
{
    public interface IPhotoService
    {
        Task<bool> NewProfilePictureAsync(IFormFile image, User user);
	}
}