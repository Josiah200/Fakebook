using Moq;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Fakebook.Core.Entities;
using Fakebook.Web.Controllers;
using Fakebook.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Fakebook.Infrastructure.Identity;
using System.Security.Claims;
using Fakebook.Web.ViewModels;

namespace Fakebook.UnitTests.Web
{
    public class HomeControllerTests
    {
		[Fact]
		public async Task Can_Use_RepositoryAsync()
		{
			// Arrange
			Mock<IPostRepository> mockRepo = new Mock<IPostRepository>();
			mockRepo.Setup(m => m.ListAllAsync()).ReturnsAsync(new List<Post> {
				new Post { PostId = 0001, AuthorId = "C56A4180-65AA-42EC-A945-5FD21DEC0538", AuthorFirst = "John", AuthorLast = "Doe", Text = "Test post data", DatePosted = DateTime.Now },
				new Post { PostId = 0002, AuthorId = "dcacebf3-1d0a-4c68-b966-2d93c83d9b58", AuthorFirst = "Sally", AuthorLast = "Smith", Text = "Second test post data", DatePosted = DateTime.Now }
			});
			
			Mock<IUserStore<ApplicationUser>> mockStore = new Mock<IUserStore<ApplicationUser>>();
			Mock<UserManager<ApplicationUser>> mockUser = new Mock<UserManager<ApplicationUser>>(mockStore.Object, null, null, null, null, null, null, null, null);
			mockUser.Setup(m => m.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(new ApplicationUser {
				UserName = "JeffW@gmail.com",
				FirstName = "Jeff",
				LastName = "White"
			});

			HomeController controller = new HomeController(mockRepo.Object, mockUser.Object);

			// Act
			HomeViewModel result = 
				(await controller.Index() as ViewResult).ViewData.Model
					as HomeViewModel;
					
			// Assert
			IReadOnlyList<Post> postList = result.Posts;
			Assert.True(postList.Count == 2);
			Assert.Equal("Test post data", postList[0].Text);
			Assert.Equal("Second test post data", postList[1].Text);
		}	
	}
}