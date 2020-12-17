using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> NewUser(string userId, string FirstName, string LastName);
    }
}