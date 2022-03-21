using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using System.Linq;

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
			post.Likes = new List<Like>();
			post.Id = Guid.NewGuid().ToString();
			post.DatePosted = DateTime.UtcNow;
			
			bool successful = await _postRepository.AddAsync(post);
			return successful;
		}

		public async Task<Post> GetByIdAsync(string postId)
		{
			return await _postRepository.GetByIdAsync(postId);
		}

		public async Task<IReadOnlyCollection<Post>?> GetHomePostsPageAsync(string userId, int page, int pageSize)
		{
			var friends = await _friendsService.GetByUserIdAsync(userId);

			List<string> userIds = new();
			userIds.Add(userId);

			foreach (Friendship friend in friends)
			{
				if (friend.UserId != userId)
				{
					userIds.Add(friend.UserId);
				}

				else
				{
					userIds.Add(friend.FriendId);
				}
			}

			var posts = await _postRepository.GetPostsPageByUserIdListAsync(userIds, page, pageSize);
			
			if (posts.Count == 0)
			{
				return null;
			}

			else
			{
				return posts;
			}
		}
		
		public async Task<IReadOnlyCollection<Post>?> GetUserPostsPageAsync(string userId, int page, int pageSize)
		{
			var userIds = new List<string> {userId};

			var posts = await _postRepository.GetPostsPageByUserIdListAsync(userIds, page, pageSize);

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