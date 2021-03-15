using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface INotificationsService
    {
        Task<List<Notification>> GetByUserIdAsync(string userId);
    }
}