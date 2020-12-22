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
		public async Task<bool> NewUser(string userId, string FirstName, string LastName)
		{
			var user = new User
			{
				Id = userId,
				FirstName = FirstName,
				LastName = LastName,
				Posts = new List<Post>()
			};

			var successful = await _userRepository.AddAsync(user);
			return successful;
		}
    }
}
