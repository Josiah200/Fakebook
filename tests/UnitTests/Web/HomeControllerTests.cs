using Moq;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Fakebook.Core.Entities;
using Fakebook.Web.Controllers;
using Fakebook.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Fakebook.UnitTests.Web
{
    public class HomeControllerTests
    {
        [Fact]
		public void Can_Use_Repository()
		{
			// Arrange
			private readonly Mock<IPostRepository> mock = new Mock<IPostRepository>();
			mock.Setup(m => m.ListAllAsync()).ReturnsAsync((new Post[] {
				new Post { PostId = 0001, AuthorId = 0001, Text = "Test post data", DatePosted = DateTime.Now },
				new Post { PostId = 0002, AuthorId = 0002, Text = "Second test post data", DatePosted = DateTime.Now }
			}).AsQueryable<Post>());

			HomeController controller = new HomeController(mock.Object);

			// Act
			IEnumerable<Post> result = 
				(controller.Index() as ViewResult).ViewData.Model
					as IEnumerable<Post>;
			// Assert
			Post[] postArray = result.ToArray();
			Assert.True(postArray.Length == 2);
			Assert.Equal("Test post data", postArray[0].Text);
			Assert.Equal("Second test post data", postArray[1].Text);
		}	
    }
}