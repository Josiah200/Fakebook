using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;

namespace Fakebook.Core.Services
{
	public class NotificationsService : INotificationsService
	{
		private readonly INotificationsRepository _notificationsRepository;
		
		public NotificationsService(INotificationsRepository notificationsRepository)
		{
			_notificationsRepository = notificationsRepository;
		}

		public async Task<List<Notification>> GetByUserIdAsync(string userId)
		{
			return await _notificationsRepository.GetByUserIdAsync(userId);
		}
	}
}