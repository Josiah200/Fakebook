using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fakebook.Core.Entities;
using System;
using Fakebook.Core.Interfaces;

namespace Fakebook.Infrastructure.Data
{
    public class ContextSeed
    {
		public static async Task SeedAllAsync(FakebookContext fakebookContext)
		{
			await fakebookContext.Users.AddRangeAsync(GetPreconfiguredUsers());
			await fakebookContext.Posts.AddRangeAsync(GetPreconfiguredPosts());
			await fakebookContext.SaveChangesAsync();
		}

		static List<User> GetPreconfiguredUsers()
		{
			return new List<User>()
			{
				new User { Id = "03a5aca4-19c1-4570-8976-2b8db05bf187", FirstName = "Joe", LastName = "White" },
				new User { Id = "047003b6-5579-4336-bc75-ada68181ab59", FirstName = "Sally", LastName = "Black" },
				new User { Id = "642ee360-5bb1-498d-9c66-3bf15e4b202c", FirstName = "Harry", LastName = "Orange" },
				new User { Id = "5249e629-bc88-4a65-8417-9f96427a2d25", FirstName= "Shaun", LastName = "Green" }
			};
		}

		static List<Post> GetPreconfiguredPosts()
		{
			return new List<Post>()
			{
				new Post { Id = Guid.NewGuid().ToString(), Text = "Seed data", DatePosted = DateTime.Now, UserId = "03a5aca4-19c1-4570-8976-2b8db05bf187" },
				new Post { Id = Guid.NewGuid().ToString(), Text = "Lorem ipsum dolor sit amet", DatePosted = DateTime.Now, UserId = "03a5aca4-19c1-4570-8976-2b8db05bf187" },
				new Post { Id = Guid.NewGuid().ToString(), Text = "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", DatePosted = DateTime.Now, UserId = "047003b6-5579-4336-bc75-ada68181ab59" },
				new Post { Id = Guid.NewGuid().ToString(), Text = "My name is Sally Black", DatePosted = DateTime.Now, UserId = "047003b6-5579-4336-bc75-ada68181ab59" },
				new Post { Id = Guid.NewGuid().ToString(), Text = "My name is Harry Orange", DatePosted = DateTime.Now, UserId = "642ee360-5bb1-498d-9c66-3bf15e4b202c" },
				new Post { Id = Guid.NewGuid().ToString(), Text = "This is a test", DatePosted = DateTime.Now, UserId = "642ee360-5bb1-498d-9c66-3bf15e4b202c" },
				new Post { Id = Guid.NewGuid().ToString(), Text = "Shaun Green's post", DatePosted = DateTime.Now, UserId = "5249e629-bc88-4a65-8417-9f96427a2d25" },
				new Post { Id = Guid.NewGuid().ToString(), Text = "Shaun Green's second post", DatePosted = DateTime.Now, UserId = "5249e629-bc88-4a65-8417-9f96427a2d25" }
			};
		}
    }
}