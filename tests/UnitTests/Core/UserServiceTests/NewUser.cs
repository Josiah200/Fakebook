using Moq;
using Fakebook.Core.Interfaces;
using Fakebook.Core.Services;
using Fakebook.Core.Entities;

namespace Fakebook.UnitTests.Core.UserServiceTests
{
    public class NewUser
    {
		private readonly Mock<IAsyncRepository<User>> _mockUserRepo;

		public NewUser()
		{
			_mockUserRepo = new Mock<IAsyncRepository<User>>();
		}
    
	}
}