using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;

namespace Fakebook.Core.Services
{
    public class PostService : IPostService
    {
		private readonly IPostRepository _postRepository;
		private readonly IUserRepository _userRepository;
		public PostService(IPostRepository postRepository, IUserRepository userRepository)
		{
			_postRepository = postRepository;
			_userRepository = userRepository;
		}
		public async Task<bool> NewPostAsync(string text, string userId)
		{
			var user = await _userRepository.GetByIdAsync(userId);

			var post = new Post
			{
				Id = Guid.NewGuid().ToString(),
				User = user,
				UserId = userId,
				Text = text,
				DatePosted = DateTime.Now
			};

			bool successful = await _postRepository.AddAsync(post);
			if (successful)
			{
				user.Posts.Add(post);
			}
			return successful;
		}
    }
}