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

		public async Task<bool> NewProfilePictureAsync(IFormFile image, User user)
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
				UserId = user.Id,
				IsProfilePicture = true
			};

			var currentPhoto = await _photoRepository.GetProfilePictureAsync(user.Id);
			var successfulAdd = await _photoRepository.AddAsync(photo);

			if(currentPhoto != null && successfulAdd == true)
			{
				var successfulDelete = await _photoRepository.DeleteAsync(currentPhoto);
				if ((successfulAdd == true) && (successfulDelete == true))
				{
					user.ProfilePicture = photoByteArray;
					await _photoRepository.SaveChangesAsync();
					return true;
				}
				return false;
			}
			user.ProfilePicture = photoByteArray;
			await _photoRepository.SaveChangesAsync();
			return successfulAdd;
		}
	}
}