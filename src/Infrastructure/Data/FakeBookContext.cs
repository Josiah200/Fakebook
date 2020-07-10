using Microsoft.EntityFrameworkCore;
using FakeBook.Core.Entities;

namespace FakeBook.Infrastructure.Data
{
    public class FakeBookContext : DbContext
    {
        public FakeBookContext(DbContextOptions<FakeBookContext> options) : base(options)
		{

		}

		public DbSet<Post> Posts { get; set; }
    }
}