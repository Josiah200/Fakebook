using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fakebook.Core.Entities;
using System;
using Bogus;
using Bogus.Extensions;
using System.Text;

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
					.RuleFor(p => p.DatePosted, f => f.Date.Past())
					.RuleFor(p => p.Likes, f => f.PickRandom(Enumerable.Range(0, r.Next(0,20)).Select(x => "AAA").ToArray(), Array.Empty<string>()));

				var usersFaker = new Faker<User>()
					.RuleFor(u => u.Id, f => Guid.NewGuid().ToString())
					.RuleFor(u => u.FirstName, f => f.Name.FirstName())
					.RuleFor(u => u.LastName, f => f.Name.LastName())
					.RuleFor(u => u.PublicId, f => f.Random.String2(3, 8))
					.RuleFor(u => u.Posts, f => postsFaker.Generate(r.Next(0, 30)).ToList().OrNull(f, 0.1f))
					.RuleFor(u => u.ProfilePicture, System.IO.File.ReadAllBytes(@"../Web/wwwroot/images/profilepicturedefault.png"))
					.RuleFor(u => u.Bio, f => f.Random.Words(r.Next(0, 30)).OrNull(f, 0.3f))
					.RuleFor(u => u.Gender, f => f.PickRandom(new string[] {"Male", "Female", "NB"}).OrNull(f, 0.3f))
					.RuleFor(u => u.Birthdate, f => f.Date.Past(100).OrNull(f, 0.4f))
					.RuleFor(u => u.City, f => f.Address.City() + ", " + f.Address.State())
					.RuleFor(u => u.Hometown, f => f.Address.City() + ", " + f.Address.State())
					.RuleFor(u => u.JobTitle, f => f.Name.JobTitle().OrNull(f, 0.2f))
					.RuleFor(u => u.Company, f => f.Company.CompanyName().OrNull(f, 0.2f))
					.RuleFor(u => u.HighSchool, f => f.PickRandom(new string[] {"Savanna High School", "Darwin High", "Blue River High", "Blue River School"}).OrNull(f, 0.2f))
					.RuleFor(u => u.College, f => f.PickRandom(new string[] {"Lone Pine College", "Mammoth College", "Saint Helena School of Fine Arts", "Storm Coast College", "White Mountain College"}).OrNull(f, 0.5f));
				var userData = usersFaker.GenerateForever().Take(500);

				await fakebookContext.Users.AddRangeAsync(userData);
				await fakebookContext.SaveChangesAsync();

			}
		}
	}
}