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
		public async Task<bool> NewPhotoAsync(IFormFile image, string userId)
		{
			var target = new MemoryStream();
			image.CopyTo(target);
			var photoByteArray = target.ToArray();

			var photo = new Photo()
			{
				Id = Guid.NewGuid().ToString(),
				PhotoByteArray = photoByteArray,
				UserId = userId
			};
			
			return await _photoRepository.AddAsync(photo);
		}
	}
}