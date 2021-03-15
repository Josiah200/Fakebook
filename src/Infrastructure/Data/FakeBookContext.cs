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

		public DbSet<User> Users { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Friendship> Friendships { get; set; }
		public DbSet<Notification> Notifications { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<User>()
				.HasMany(u => u.Posts)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Friendship>()
				.HasKey(f => new { f.UserId, f.FriendId });

			builder.Entity<User>()
				.HasMany(u => u.Friendships)
				.WithOne(u => u.Friend)
				.OnDelete(DeleteBehavior.Restrict);
		}
    }
}
