using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IUserService
    {
		Task<bool> NewUserAsync(string userId, string firstName, string lastName, string? gender, DateTime? birthdate, byte[] profilePicture = null);
		Task<User> GetByIdAsync(string userId);
		Task<User> GetByPublicIdAsync(string publicId);
		Task<List<User>> GetPageAsync(string? searchString, int page = 0);
		Task<bool> UpdateProfileAsync(User currentUser, User userInput);
		Task<bool> UpdatePublicIdAsync(User currentUser, string publicIdInput);
	}
}