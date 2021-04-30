using Microsoft.EntityFrameworkCore;
using Fakebook.Core.Entities;
using System.Reflection;
using System;

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

			builder.Entity<Post>()
				.Property(p => p.Likes)
				.HasConversion(
					l => string.Join(',', l),
					l => l.Split(',', StringSplitOptions.RemoveEmptyEntries)
				);

			builder.Entity<Photo>()
				.HasOne(p => p.Post)
				.WithOne(p => p.Photo)
				.HasForeignKey<Photo>(p => p.PostId);

			builder.Entity<Friendship>()
				.HasKey(f => new { f.UserId, f.FriendId });
		}
    }
}
