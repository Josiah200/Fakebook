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
    public class GetUserPostsPageAsync
    {
		private readonly Mock<IPostRepository> _mockPostRepo;
		private readonly Mock<IFriendsService> _mockFriendsService;
		private readonly List<Post> _userPostData;
		private readonly User _testUser;
		
		public GetUserPostsPageAsync()
		{
			_mockPostRepo = new Mock<IPostRepository>();
			_mockFriendsService = new Mock<IFriendsService>();
			_testUser = new User()
			{
				PublicId = "TESTUSER"
			};

			_userPostData = new List<Post>()
			{
				new Post() {User = _testUser, Text = "Test Text1", DatePosted = DateTime.Now},
				new Post() {User = _testUser, Text = "Test Text2", DatePosted = DateTime.Now},
				new Post() {User = _testUser, Text = "Test Text3", DatePosted = DateTime.Now},
			};
		}

        [Fact]
		public async Task InvokesPostRepositoryOnce()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_userPostData);
			var postService = new PostService(_mockPostRepo.Object, null, _mockFriendsService.Object);

			await postService.GetUserPostsPageAsync("TESTUSER", 0, 16);

			_mockPostRepo.Verify(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
		}

		[Fact]
		public async Task InvokesPostRepositoryWithListOfUserId()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_userPostData);
			var postService = new PostService(_mockPostRepo.Object, null, _mockFriendsService.Object);

			var result = await postService.GetUserPostsPageAsync("TESTUSER", 0, 16);
			var required = new List<string> {"TESTUSER"};

			_mockPostRepo.Verify(x => x.GetPostsPageByUserIdListAsync(required, It.IsAny<int>(), It.IsAny<int>()));
		}

		[Fact]
		public async Task ReturnsListOfPosts()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_userPostData);
			var postService = new PostService(_mockPostRepo.Object, null, _mockFriendsService.Object);

			var result = await postService.GetUserPostsPageAsync("TESTUSER", 0, 16);	

			Assert.IsType<List<Post>>(result);
		}

		[Fact]
		public async Task ReturnsUsersPosts()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_userPostData);
			var postService = new PostService(_mockPostRepo.Object, null, _mockFriendsService.Object);

			var result = await postService.GetUserPostsPageAsync("TESTUSER", 0, 16);	

			result.Should().Contain(_userPostData);
		}

		[Fact]
		public async Task ReturnsNullIfNoPostsFound()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Post>().AsReadOnly());
			var postService = new PostService(_mockPostRepo.Object, null, _mockFriendsService.Object);

			var result = await postService.GetUserPostsPageAsync("TESTUSER", 0, 16);	

			result.Should().BeNull();
		}
    }
}