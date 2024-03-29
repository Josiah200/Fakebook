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
				new Post() {UserId = "ba6bd0bf-7cee-4a7f-ba72-fbd41ce4540b", Text = "User Text1", DatePosted = DateTime.Now},
				new Post() {UserId = "ba6bd0bf-7cee-4a7f-ba72-fbd41ce4540b", Text = "User Text2", DatePosted = DateTime.Now},
				new Post() {UserId = "ba6bd0bf-7cee-4a7f-ba72-fbd41ce4540b", Text = "User Text3", DatePosted = DateTime.Now},
			};

			_friendsPostData = new List<Post>
			{
				new Post() {UserId = "8fd20962-e899-48cf-8c67-a5ced8716fe9", Text = "Friend Text1", DatePosted = DateTime.Now},
				new Post() {UserId = "8fd20962-e899-48cf-8c67-a5ced8716fe9", Text = "Friend Text2", DatePosted = DateTime.Now},
				new Post() {UserId = "8fd20962-e899-48cf-8c67-a5ced8716fe9", Text = "Friend Text3", DatePosted = DateTime.Now},
				new Post() {UserId = "d8e400e7-ede3-4485-ab6b-3138d371fe80", Text = "Friend2 Text1", DatePosted = DateTime.Now},
				new Post() {UserId = "d8e400e7-ede3-4485-ab6b-3138d371fe80", Text = "Friend2 Text2", DatePosted = DateTime.Now},
				new Post() {UserId = "d8e400e7-ede3-4485-ab6b-3138d371fe80", Text = "Friend2 Text3", DatePosted = DateTime.Now},
				new Post() {UserId = "f66f7807-657c-4c49-b101-2e294a615429", Text = "Friend3 Text1", DatePosted = DateTime.Now},
				new Post() {UserId = "f66f7807-657c-4c49-b101-2e294a615429", Text = "Friend3 Text2", DatePosted = DateTime.Now},
				new Post() {UserId = "f66f7807-657c-4c49-b101-2e294a615429", Text = "Friend3 Text3", DatePosted = DateTime.Now}
			};

			_allPostData = new List<Post>();
			_allPostData.AddRange(_userPostData);
			_allPostData.AddRange(_friendsPostData);

			_friendInMemoryDb = new List<Friendship>
			{
				new Friendship() {UserId = "ba6bd0bf-7cee-4a7f-ba72-fbd41ce4540b", FriendId = "FRIENDUSER"}
			};

			_mockFriendsService.Setup(x => x.GetByUserIdAsync(It.IsAny<string>())).ReturnsAsync(_friendInMemoryDb);
		}

        [Fact]
		public async Task InvokesPostRepositoryOnce()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_allPostData);
			var postService = new PostService(_mockPostRepo.Object);

			await postService.GetHomePostsPageAsync("ba6bd0bf-7cee-4a7f-ba72-fbd41ce4540b", 0, 16, null);

			_mockPostRepo.Verify(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
		}
		
		[Fact]
		public async Task ReturnsListOfPosts()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_allPostData);
			var postService = new PostService(_mockPostRepo.Object);

			var result = await postService.GetHomePostsPageAsync("ba6bd0bf-7cee-4a7f-ba72-fbd41ce4540b", 0, 16, null);	

			Assert.IsType<List<Post>>(result);
		}

		[Fact]
		public async Task ReturnsCurrentUsersPosts()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_allPostData);
			var postService = new PostService(_mockPostRepo.Object);

			var result = await postService.GetHomePostsPageAsync("ba6bd0bf-7cee-4a7f-ba72-fbd41ce4540b", 0, 16, null);	

			result.Should().Contain(_userPostData);
		}

		[Fact]
		public async Task ReturnsFriendsPosts()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_allPostData);
			var postService = new PostService(_mockPostRepo.Object);

			var result = await postService.GetHomePostsPageAsync("ba6bd0bf-7cee-4a7f-ba72-fbd41ce4540b", 0, 16, null);	

			result.Should().Contain(_friendsPostData);
		}

		[Fact]
		public async Task ReturnsNullIfNoPostsFound()
		{
			_mockPostRepo.Setup(x => x.GetPostsPageByUserIdListAsync(It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<Post>().AsReadOnly());
			var postService = new PostService(_mockPostRepo.Object);

			var result = await postService.GetHomePostsPageAsync("ba6bd0bf-7cee-4a7f-ba72-fbd41ce4540b", 0, 16, null);	

			result.Should().BeNull();
		}
    }
}