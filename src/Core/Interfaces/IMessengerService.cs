using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IMessengerService
    {
        Task<bool> SaveConnectionAsync(string userId, string connectionId);
		Task<bool> DeleteConnectionAsync(string connectionId);
		Task<List<Connection>> GetConnectionsAsync(string userId);
    }
}