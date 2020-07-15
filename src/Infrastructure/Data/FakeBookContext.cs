using Microsoft.EntityFrameworkCore;
using Fakebook.Core.Entities;
using System.Reflection;

namespace Fakebook.Infrastructure.Data
{
    public class FakebookContext : DbContext
    {
        public FakebookContext(DbContextOptions<FakebookContext> options) : base(options)
		{
		}

		public DbSet<Post> Posts { get; set; }

    }
}