using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fakebook.Core.Entities;
using System;

namespace Fakebook.Infrastructure.Data
{
    public class ContextSeed
    {
		public static async Task SeedAllAsync(FakebookContext fakebookContext)
		{
			if (!fakebookContext.Posts.Any())
			{
				await SeedPosts(fakebookContext);
			}
		}

		private static async Task SeedPosts(FakebookContext fakebookContext)
		{
			var posts = new[]
			{
				new Post { PostId = 0001, AuthorId = 0001, Text = "Seed data", DatePosted = DateTime.Now },
				new Post { PostId = 0002, AuthorId = 0002, Text = "Lorem ipsum dolor sit amet", DatePosted = DateTime.Now },
				new Post { PostId = 0003, AuthorId = 0002, Text = "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", DatePosted = DateTime.Now }
			};
			fakebookContext.Posts.AddRange(posts);
			await fakebookContext.SaveChangesAsync();
		}
    }
}