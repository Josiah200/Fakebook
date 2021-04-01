using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Core.Services;
using Moq;
using Xunit;

namespace Tests.Core.Services
{
    public class PostServiceTests
    {
		private readonly Mock<IPostRepository> _mockPostRepo;
		private readonly Mock<IFriendsService> _mockFriendsService;
		private readonly List<Post> _postInMemoryDb;
		private readonly List<Friendship> _friendInMemoryDb;
		
		public PostServiceTests()
		{
			_mockPostRepo = new Mock<IPostRepository>();
			_mockFriendsService = new Mock<IFriendsService>();
			_postInMemoryDb = new List<Post>
			{
				new Post() {UserId = "TESTUSER", Text = "Test Text1", DatePosted = DateTime.Now},
				new Post() {UserId = "TESTUSER", Text = "Test Text2", DatePosted = DateTime.Now},
				new Post() {UserId = "TESTUSER", Text = "Test Text3", DatePosted = DateTime.Now},
				new Post() {UserId = "FRIENDUSER", Text = "Friend Text1", DatePosted = DateTime.Now},
				new Post() {UserId = "FRIENDUSER", Text = "Friend Text2", DatePosted = DateTime.Now},
				new Post() {UserId = "FRIENDUSER", Text = "Friend Text3", DatePosted = DateTime.Now}
			};

			_friendInMemoryDb = new List<Friendship>
			{
				new Friendship() {UserId = "TESTUSER", FriendId = "FRIENDUSER"}
			};
		}

        [Fact]
		public async Task GetHomePostsBlockAsyncInvokesRepositoryOnce()
		{
			_mockPostRepo.Setup(x => x.GetHomePostsBlockAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_postInMemoryDb);

			_mockFriendsService.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync(_friendInMemoryDb);

			var postService = new PostService(_mockPostRepo.Object, _mockFriendsService.Object);

			await postService.GetHomePostsBlockAsync(10, 10, "TESTUSER");

			_mockPostRepo.Verify(x => x.GetHomePostsBlockAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
		}
    }
}