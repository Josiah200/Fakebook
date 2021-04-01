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
		private readonly IFriendsService _friendsService;

		public PostService(IPostRepository postRepository, IFriendsService friendsService)
		{
			_postRepository = postRepository;
			_friendsService = friendsService;
		}

		public async Task<bool> SavePostAsync(Post post)
		{
			if (post is null)
			{
				throw new ArgumentNullException(nameof(post));
			}
			bool successful = await _postRepository.AddAsync(post);
			return successful;
		}

		public async Task<IReadOnlyCollection<Post>?> GetHomePostsBlockAsync(int page, int blockSize, string userId)
		{
			var friends = await _friendsService.GetByUserIdAsync(userId);

			List<string> friendIds = new();
			friendIds.Add(userId);

			foreach (Friendship friend in friends)
			{
				if (friend.UserId != userId)
				{
					friendIds.Add(friend.UserId);
				}

				else
				{
					friendIds.Add(friend.FriendId);
				}
			}

			var posts = await _postRepository.GetHomePostsBlockAsync(friendIds, page, blockSize);
			
			if (posts.Count == 0)
			{
				return null;
			}

			else
			{
				return posts;
			}
		}
		
		public async Task<IReadOnlyCollection<Post>?> GetUserPostsBlockAsync(int page, int blockSize, string userPublicId)
		{
			var posts = await _postRepository.GetUserPostsBlockByPublicIdAsync(userPublicId, page, blockSize);

			if (posts.Count == 0)
			{
				return null;
			}

			else
			{
				return posts;
			}
		}
	}
}