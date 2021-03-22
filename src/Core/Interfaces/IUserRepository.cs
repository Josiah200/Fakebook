using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IUserRepository : IAsyncRepository<User>
    {
		Task<User> GetByPublicIdAsync(string userPublicId);
		Task<List<User>> GetChunkAsync(int page);
    }
}