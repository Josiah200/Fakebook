namespace FakeBook.Infrastructure.Data
{
    public class EfRepository<T>
    {
		protected readonly FakeBookContext _dbContext;
        public EfRepository(FakeBookContext dbContext)
		{
			_dbContext = dbContext;
		}
    }
}