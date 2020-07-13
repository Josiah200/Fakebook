namespace Fakebook.Infrastructure.Data
{
    public class EfRepository<T>
    {
		protected readonly FakebookContext _dbContext;
        public EfRepository(FakebookContext dbContext)
		{
			_dbContext = dbContext;
		}
    }
}