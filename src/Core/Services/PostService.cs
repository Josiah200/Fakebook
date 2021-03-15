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

		public async Task<bool> SavePostAsync(Post post)
		{
			if (post is null)
			{
				throw new ArgumentNullException(nameof(post));
			}
			// post.LastModified = DateTime.Now;
			
			bool successful = await _postRepository.AddAsync(post);
			return successful;
		}
		
		public async Task<List<Post>> GetPostsBlockAsync(int page, int blockSize)
		{
			return await _postRepository.GetPostsBlockAsync(page, blockSize);
		}

		public async Task<List<Post>> GetUserPostsBlockAsync(string userId, int page, int blockSize)
		{
			return await _postRepository.GetUserPostsBlockAsync(userId, page, blockSize);
		}
    }
}