using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Fakebook.Core.Services
{
	public class PhotoService : IPhotoService
	{
		private readonly IAsyncRepository<Photo> _photoRepository;
		
		public PhotoService(IAsyncRepository<Photo> photoRepository)
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
            }

			var photo = new Photo()
			{
				Id = Guid.NewGuid().ToString(),
				PhotoByteArray = photoByteArray,
				UserId = user.Id,
				IsProfilePicture = true
			};

			var photos = await _photoRepository.ListAllAsync();
			
			Photo? currentPhoto = photos
				.Where(p => p.UserId == user.Id)
				.FirstOrDefault(p => p.IsProfilePicture);

			var successfulAdd = await _photoRepository.AddAsync(photo);

			if(currentPhoto != null && successfulAdd)
			{
				var successfulDelete = await _photoRepository.DeleteAsync(currentPhoto);
				if (successfulAdd && successfulDelete)
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