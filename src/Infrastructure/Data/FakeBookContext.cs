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
			Database.EnsureCreated();
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<Photo> Photos { get; set; }
		public DbSet<Friendship> Friendships { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<Connection> Connections { get; set; }
		public DbSet<Message> Messages { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			builder.Entity<User>()
				.HasMany(u => u.Posts)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<User>()
				.HasMany(u => u.Photos)
				.WithOne(p => p.User)
				.HasForeignKey(p => p.UserId);

			builder.Entity<User>()
				.HasMany(u => u.Friendships)
				.WithOne(u => u.Friend)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<User>()
				.HasMany(u => u.Comments)
				.WithOne(c => c.User)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.Restrict);
			
			builder.Entity<Photo>()
				.HasOne(p => p.Post)
				.WithOne(p => p.Photo)
				.HasForeignKey<Photo>(p => p.PostId);

			builder.Entity<Like>()
				.HasKey(l => new { l.UserId, l.PostId });
				
			builder.Entity<Post>()
				.HasMany(p => p.Likes)
				.WithOne(l => l.Post)
				.HasForeignKey(l => l.PostId)
				.OnDelete(DeleteBehavior.ClientCascade);

			builder.Entity<Comment>()
				.HasMany(c => c.Likes)
				.WithOne(l => l.Comment)
				.HasForeignKey(l => l.PostId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Comment>()
				.HasMany(c => c.Replies)
				.WithOne(r => r.Parent)
				.HasForeignKey(r => r.ParentCommentId);

			builder.Entity<Friendship>()
				.HasKey(f => new { f.UserId, f.FriendId });

			builder.Entity<Message>()
				.HasOne(m => m.Sender)
				.WithMany(u => u.SentMessages)
				.HasForeignKey(m => m.SenderId);

			builder.Entity<Message>()
				.HasOne(m => m.Reciever)
				.WithMany(u => u.RecievedMessages)
				.HasForeignKey(m => m.RecieverId);
		}
    }
}