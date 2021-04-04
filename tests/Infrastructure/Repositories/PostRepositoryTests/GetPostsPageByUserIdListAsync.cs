using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace tests.Infrastructure.Repositories.PostRepositoryTests
{
    public class GetPostsPageByUserIdListAsync
    {
		private readonly FakebookContext _dbContext;
		private readonly PostRepository _postRepository;
		public GetPostsPageByUserIdListAsync()
		{
			var dbOptions = new DbContextOptionsBuilder<FakebookContext>()
				.UseInMemoryDatabase(databaseName: "TestPosts").Options;

			_dbContext = new FakebookContext(dbOptions);
			_postRepository = new PostRepository(_dbContext);
		}

		[Fact]
		public async Task ReturnsExistingListOfPosts()
		{
			var existingPosts = new List<Post>
			{
				new Post {Id = "A", UserId = "TESTUSER", Text = "Test text"},
				new Post {Id = "B", UserId = "TESTUSER", Text = "Test text"},
				new Post {Id = "C", UserId = "TESTUSER", Text = "Test text"},
				new Post {Id = "D", UserId = "TESTUSER", Text = "Test text"},
				new Post {Id = "E", UserId = "TESTUSER2", Text = "Test text2"},
				new Post {Id = "F", UserId = "TESTUSER2", Text = "Test text2"},
				new Post {Id = "G", UserId = "TESTUSER2", Text = "Test text2"},
				new Post {Id = "H", UserId = "TESTUSER2", Text = "Test text2"},
				new Post {Id = "I", UserId = "TESTUSER3", Text = "Test text3"},
				new Post {Id = "J", UserId = "TESTUSER3", Text = "Test text3"},
				new Post {Id = "K", UserId = "TESTUSER3", Text = "Test text3"},
				new Post {Id = "L", UserId = "TESTUSER3", Text = "Test text3"}
			};
			var users = new List<User>
			{
				new User {Id = "TESTUSER"},
				new User {Id = "TESTUSER2"},
				new User {Id = "TESTUSER3"}
			};

			_dbContext.Posts.AddRange(existingPosts);
			_dbContext.Users.AddRange(users);
			_dbContext.SaveChanges();
			var userIds = new List<string>
			{
				"TESTUSER",
				"TESTUSER2"
			};
			var result = await _postRepository.GetPostsPageByUserIdListAsync(userIds, 0, 16);

			var expected = existingPosts.GetRange(0,8);
			for(int i = 0; i < 7; i++)
			{
				Assert.Equal(result[i].Id, expected[i].Id);
			}
		}
	}
}