using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fakebook.Infrastructure.Data
{
    public class NotificiationsRepository : EfRepository<Notification>, INotificationsRepository
    {
        public NotificiationsRepository(FakebookContext dbContext) : base(dbContext)
		{
		}

		public Task<List<Notification>> GetByUserIdAsync(string userId)
		{
			return _dbContext.Notifications
			.Where(n => n.ReceiverId == userId)
			.ToListAsync();
		}
	}
}