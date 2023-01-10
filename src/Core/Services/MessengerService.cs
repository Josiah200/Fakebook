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
		
		public MessengerService(IAsyncRepository<Connection> connectionRepository)
		{
			_connectionRepository = connectionRepository;
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
	}
}