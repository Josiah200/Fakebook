using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;

namespace Fakebook.Core.Services
{
	public class MessengerService : IMessengerService
	{
		private readonly IAsyncRepository<Connection> _connectionRepository;
		private readonly IAsyncRepository<Message> _messageRepository;
		
		public MessengerService(IAsyncRepository<Connection> connectionRepository , IAsyncRepository<Message> messageRepository)
		{
			_connectionRepository = connectionRepository;
			_messageRepository = messageRepository;
		}

		public async Task<bool> SaveConnectionAsync(string userId, string connectionId)
		{
			var connection = new Connection()
			{
				Id = connectionId,
				UserId = userId
			};
			return await _connectionRepository.AddAsync(connection);
		}
		
		public async Task<bool> DeleteConnectionAsync(string connectionId)
		{
			Connection connection = await _connectionRepository.GetByIdAsync(connectionId);
			return await _connectionRepository.DeleteAsync(connection);
		}

		public async Task<List<Connection>> GetConnectionsAsync(string userId)
		{
			var lst = await _connectionRepository.ListAllAsync();
			return lst.Where(p => p.UserId == userId).ToList();
		}

		public async Task<bool> SaveMessageAsync(Message message)
		{
			return await _messageRepository.AddAsync(message);
		}
		
		public async Task<List<Message>> GetAllMessagesAsync(string firstUserId, string secondUesrId)
		{
			var lst = await _messageRepository.ListAllAsync();
			return lst.Where(p => (p.SenderId == firstUserId && p.RecieverId == secondUesrId) || (p.SenderId == secondUesrId && p.RecieverId == firstUserId)).ToList();
		}
	}
}