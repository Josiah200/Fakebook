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
		public PostService(IPostRepository postRepository)
		{
			_postRepository = postRepository;
		}
		public async Task<bool> NewPostAsync(string text, string userId)
		{
			var post = new Post
			{
				Id = Guid.NewGuid().ToString(),
				UserId = userId,
				Text = text,
				DatePosted = DateTime.Now
			};

			bool successful = await _postRepository.AddAsync(post);
			return successful;
		}
    }
}