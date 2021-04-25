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
		public DbSet<Photo> Photos { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<Friendship> Friendships { get; set; }
		public DbSet<Notification> Notifications { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			builder.Entity<User>()
				.HasMany(u => u.Posts)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<User>()
				.HasMany(u => u.Photos)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId);

			builder.Entity<User>()
				.HasMany(u => u.Friendships)
				.WithOne(u => u.Friend)
				.OnDelete(DeleteBehavior.Restrict);
			
			builder.Entity<User>()
				.HasMany(u => u.Likes)
				.WithOne(l => l.User)
				.HasForeignKey(l => l.UserId)
				.OnDelete(DeleteBehavior.ClientCascade);
			
			builder.Entity<Post>()
				.HasMany(u => u.Likes)
				.WithOne(l => l.Post)
				.HasForeignKey(l => l.PostId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Entity<Photo>()
				.HasOne(p => p.Post)
				.WithOne(p => p.Photo)
				.HasForeignKey<Photo>(p => p.PostId);

			builder.Entity<Friendship>()
				.HasKey(f => new { f.UserId, f.FriendId });
		}
    }
}
