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
				new Post { PostId = 0001, AuthorId = 0001, Text = "Test post data", DatePosted = DateTime.Now },
				new Post { PostId = 0002, AuthorId = 0002, Text = "Second test post data", DatePosted = DateTime.Now }
			});

			HomeController controller = new HomeController(mockRepo.Object);

			// Act
			List<Post> result = 
				(await controller.Index() as ViewResult).ViewData.Model
					as List<Post>;
					
			// Assert
			List<Post> postList = result.ToList();
			Assert.True(postList.Count == 2);
			Assert.Equal("Test post data", postList[0].Text);
			Assert.Equal("Second test post data", postList[1].Text);
		}	
	}
}