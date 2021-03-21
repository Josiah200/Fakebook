using System;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IUserService
    {
		Task<bool> NewUserAsync(string userId, string FirstName, string LastName, string PublicId);
		Task<User> GetByIdAsync(string userId);
		Task<User> GetByPublicIdAsync(string publicId);
		Task<string> GenerateRandomPublicIdAsync();
	}
}