using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fakebook.Core.Entities;
using System;
using Bogus;

namespace Fakebook.Infrastructure.Data
{
    public class ContextSeed
    {
		public static async Task SeedAllAsync(FakebookContext fakebookContext)
		{
			if (!await fakebookContext.Users.AnyAsync())
			{	
				var r = new Random();

				var postsFaker = new Faker<Post>()
					.RuleFor(p => p.Id, f => Guid.NewGuid().ToString())
					.RuleFor(p => p.Text, f => f.Rant.Random.Words(r.Next(5, 200)))
					.RuleFor(p => p.DatePosted, f => f.Date.Past());

				var usersFaker = new Faker<User>()
					.RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
					.RuleFor(u => u.FirstName, f => f.Name.FirstName())
					.RuleFor(u => u.LastName, f => f.Name.LastName())
					.RuleFor(u => u.PublicId, f => f.Random.String2(3, 8))
					.RuleFor(u => u.Posts, f => postsFaker.Generate(r.Next(0, 30)).ToList());
				
				var userData = usersFaker.GenerateForever().Take(500);

				await fakebookContext.Users.AddRangeAsync(userData);
				await fakebookContext.SaveChangesAsync();

			}
		}
	}
}