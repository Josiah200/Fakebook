using System;
using System.IO;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Fakebook.Core.Services
{
	public class PhotoService : IPhotoService
	{
		private readonly IPhotoRepository _photoRepository;
		
		public PhotoService(IPhotoRepository photoRepository)
		{
			_photoRepository = photoRepository;
		}

		public async Task<bool> NewPhotoAsync(IFormFile image, string userId, bool isProfilePicture = false)
		{
			byte[] photoByteArray;
			using (var memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                photoByteArray = memoryStream.ToArray();
            };

			var photo = new Photo()
			{
				Id = Guid.NewGuid().ToString(),
				PhotoByteArray = photoByteArray,
				UserId = userId,
				IsProfilePicture = isProfilePicture
			};
			if (photo.IsProfilePicture == true)
			{
				var currentPhoto = await _photoRepository.GetProfilePhotoAsync(userId);
				await _photoRepository.DeleteAsync(currentPhoto);
				return await _photoRepository.AddAsync(photo);
			}
			else
			{
				return await _photoRepository.AddAsync(photo);
			}
		}
	}
}