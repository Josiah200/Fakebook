using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;

namespace Fakebook.Core.Services
{
    public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		
		public async Task<bool> NewUserAsync(string userId, string firstName, string lastName, string? gender, DateTime? birthdate)
		{
			var user = new User
			{
				Id = userId,
				FirstName = firstName,
				LastName = lastName,
				PublicId = await GenerateRandomPublicIdAsync(),
				HasAvatar = false,
				Gender = gender,
				Birthdate = birthdate
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
				if ((oldvalue != newvalue) && (property.Name != "Id"))
				{
					property.SetValue(currentUser, newvalue);
				}
			}
			return await _userRepository.UpdateAsync(currentUser);
		}

		public async Task<List<User>> GetPageAsync(string? searchString, int page = 0)
		{
			return await _userRepository.GetPageAsync(searchString ?? "", page);
		}

		private async Task<string> GenerateRandomPublicIdAsync()
		{
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-";
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
