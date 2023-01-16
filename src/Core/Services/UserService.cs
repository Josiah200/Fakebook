using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Fakebook.Core.Services
{
    public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IHostingEnvironment _hostingEnvironment;

        public UserService(IUserRepository userRepository, IHostingEnvironment hostingEnvironment)
		{
			_userRepository = userRepository;
			_hostingEnvironment = hostingEnvironment;
		}
		
		public async Task<bool> NewUserAsync(string userId, string firstName, string lastName, string? gender, DateTime? birthdate, byte[] profilePicture = null)
		{
			var path = Path.Combine(_hostingEnvironment.WebRootPath, "images/profilepicturedefault.png");
			Stream pictureStream = System.IO.File.OpenRead(path);
			byte[] defaultPicture;
			using (BinaryReader br = new(pictureStream))
			{
				defaultPicture = br.ReadBytes((int)pictureStream.Length);
			}

			var user = new User
			{
				Id = userId,
				FirstName = firstName,
				LastName = lastName,
				ProfilePicture = profilePicture ?? defaultPicture,
				PublicId = await GenerateRandomPublicIdAsync(),
				Gender = gender,
				Birthdate = birthdate,
			};

			var successful = await _userRepository.AddAsync(user);
			return successful;
		}
		
		public async Task<User> GetByIdAsync(string Id)
		{
			return await _userRepository.GetByIdAsync(Id);
		}
		
		public async Task<User> GetByPublicIdAsync(string publicId)
		{
			return await _userRepository.GetByPublicIdAsync(publicId);
			
		}
		
		public async Task<bool> UpdateProfileAsync(User currentUser, User userInput)
		{
			foreach (var property in currentUser.GetType().GetProperties())
			{
				var oldvalue = property.GetValue(currentUser);
				var newvalue = userInput.GetType().GetProperty(property.Name).GetValue(userInput);
				if ((oldvalue != newvalue) && (property.Name != "Id") && (property.Name != "ProfilePicture") && (property.Name != "PublicId"))
				{
					property.SetValue(currentUser, newvalue);
				}
			}
			return await _userRepository.UpdateAsync(currentUser);
		}

		public async Task<bool> UpdatePublicIdAsync(User currentUser, string publicIdInput)
		{
			if(await _userRepository.GetByPublicIdAsync(publicIdInput) != null)
			{
				return false;
			}

			currentUser.PublicId = publicIdInput;

			return await _userRepository.UpdateAsync(currentUser);
		}

		public async Task<List<User>> GetPageAsync(string? searchString, int page = 0)
		{
			if (!String.IsNullOrEmpty(searchString))
			{
				searchString = searchString.ToLower();
			}
			return await _userRepository.GetPageAsync(searchString ?? "", page);
		}

		private async Task<string> GenerateRandomPublicIdAsync()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-";
			var random = new Random();
			string id;
			
			while(true)
			{
				var stringChars = new char[random.Next(3, 8)];

				for (int i = 0; i < stringChars.Length; i++)
				{
					stringChars[i] = chars[random.Next(chars.Length)];
				}

				id = new string(stringChars);
				var user = await _userRepository.GetByPublicIdAsync(id);
				
				if (user is null)
				{
					return id;
				}
			}
		}
    }
}
