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
				new Post { Text = "Seed data", AuthorFirst = "Firsttest", AuthorLast = "Lasttest", DatePosted = DateTime.Now },
				new Post { Text = "Lorem ipsum dolor sit amet", AuthorFirst = "Firsttest", AuthorLast = "Lasttest", DatePosted = DateTime.Now },
				new Post { Text = "consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", AuthorFirst = "Firsttest", AuthorLast = "Lasttest", DatePosted = DateTime.Now }
			};
			fakebookContext.Posts.AddRange(posts);
			await fakebookContext.SaveChangesAsync();
		}
    }
}