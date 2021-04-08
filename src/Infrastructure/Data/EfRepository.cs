using Fakebook.Core.Interfaces;
using Fakebook.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Fakebook.Infrastructure.Data
{
	public class EfRepository<T> where T : BaseEntity
    {
		protected readonly FakebookContext _dbContext;
		
        public EfRepository(FakebookContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<T> GetByIdAsync(string id)
		{
			return await _dbContext.Set<T>()
				.FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}
		
		public async Task<bool> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			var addResult = await _dbContext.SaveChangesAsync();
			return addResult == 1;
		}
		
		public async Task<bool> DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			var deleteResult = await _dbContext.SaveChangesAsync();
			return deleteResult == 1;
		}
		
		public async Task<bool> UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            var updateResult = await _dbContext.SaveChangesAsync();
			return updateResult == 1;
        }
    }
}