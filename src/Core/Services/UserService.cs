using System;
using System.Collections.Generic;
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
		
		public async Task<bool> NewUserAsync(string userId, string firstName, string lastName, string publicId)
		{
			var user = new User
			{
				Id = userId,
				FirstName = firstName,
				LastName = lastName,
				PublicId = publicId,
				Posts = new List<Post>(),
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
    }
}
