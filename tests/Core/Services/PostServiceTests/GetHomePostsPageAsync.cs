using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Core.Services;
using Moq;
using Xunit;
using FluentAssertions;

namespace Tests.Core.Services.PostServiceTests
{
    public class GetHomePostsPageAsync
    {
		private readonly Mock<IPostRepository> _mockPostRepo;
		private readonly Mock<IFriendsService> _mockFriendsService;
		private readonly List<Post> _userPostData;
		private readonly List<Post> _friendsPostData;
		private readonly List<Post> _allPostData;
		private readonly List<Friendship> _friendInMemoryDb;
		
		public GetHomePostsPageAsync()
		{
			_mockPostRepo = new Mock<IPostRepository>();
			_mockFriendsService = new Mock<IFriendsService>();
			_userPostData = new List<Post>
			{
				new Post() {UserId = "TESTUSER", Text = "Test Text1", DatePosted = DateTime.Now},
				new Post() {UserId = "TESTUSER", Text = "Test Text2", DatePosted = DateTime.Now},
				new Post() {UserId = "TESTUSER", Text = "Test Text3", DatePosted = DateTime.Now},
			};

			_friendsPostData = new List<Post>
			{
				new Post() {UserId = "FRIENDUSER", Text = "Friend Text1", DatePosted = DateTime.Now},
				new Post() {UserId = "FRIENDUSER", Text = "Friend Text2", DatePosted = DateTime.Now},
				new Post() {UserId = "FRIENDUSER", Text = "Friend Text3", DatePosted = DateTime.Now},
				new Post() {UserId = "FRIENDUSER2", Text = "Friend2 Text1", DatePosted = DateTime.Now},
				new Post() {UserId = "FRIENDUSER2", Text = "Friend2 Text2", DatePosted = DateTime.Now},
				new Post() {UserId = "FRIENDUSER2", Text = "Friend2 Text3", DatePosted = DateTime.Now},
				new Post() {UserId = "FRIENDUSER3", Text = "Friend3 Text1", DatePosted = DateTime.Now},
				new Post() {UserId = "FRIENDUSER3", Text = "Friend3 Text2", DatePosted = DateTime.Now},
				new Post() {UserId = "FRIENDUSER3", Text = "Friend3 Text3", DatePosted = DateTime.Now}
			};

			_allPostData = new List<Post>
			{
			};
			_allPostData.AddRange(_userPostData);
			_allPostData.AddRange(_friendsPostData);

			_friendInMemoryDb = new List<Friendship>
			{
				new Friendship() {UserId = "TESTUSER", FriendId = "FRIENDUSER"}
			};
		}

        [Fact]
		public async Task InvokesPostRepositoryOnce()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_allPostData);
			_mockFriendsService.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync(_friendInMemoryDb);
			var postService = new PostService(_mockPostRepo.Object, _mockFriendsService.Object);

			await postService.GetHomePostsPageAsync("TESTUSER", 0, 16);

			_mockPostRepo.Verify(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
		}
		
		[Fact]
		public async Task ReturnsListOfPosts()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_allPostData);
			_mockFriendsService.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync(_friendInMemoryDb);
			var postService = new PostService(_mockPostRepo.Object, _mockFriendsService.Object);

			var result = await postService.GetHomePostsPageAsync("TESTUSER", 0, 16);	

			Assert.IsType<List<Post>>(result);
		}

		[Fact]
		public async Task ReturnsCurrentUsersPosts()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_allPostData);
			_mockFriendsService.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync(_friendInMemoryDb);
			var postService = new PostService(_mockPostRepo.Object, _mockFriendsService.Object);

			var result = await postService.GetHomePostsPageAsync("TESTUSER", 0, 16);	

			result.Should().Contain(_userPostData);
		}

		[Fact]
		public async Task ReturnsFriendsPosts()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_allPostData);
			_mockFriendsService.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync(_friendInMemoryDb);
			var postService = new PostService(_mockPostRepo.Object, _mockFriendsService.Object);

			var result = await postService.GetHomePostsPageAsync("TESTUSER", 0, 16);	

			result.Should().Contain(_friendsPostData);
		}

		[Fact]
		public async Task ReturnsNullIfNoPostsFound()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Post>().AsReadOnly());
			_mockFriendsService.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync(_friendInMemoryDb);
			var postService = new PostService(_mockPostRepo.Object, _mockFriendsService.Object);

			var result = await postService.GetHomePostsPageAsync("TESTUSER", 0, 16);	

			result.Should().BeNull();
		}
    }
}