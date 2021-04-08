using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;

namespace Fakebook.Core.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
		Task<T> GetByIdAsync(string id);
        Task<IReadOnlyList<T>> ListAllAsync();
		Task<bool> AddAsync(T entity);
		Task<bool> DeleteAsync(T entity);
		Task<bool> UpdateAsync(T entity);
    }
}