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
			//_userRepository = userRepository;
		}
		public async Task<bool> NewPost(string text, string authorId, IApplicationUser author)
		{
			var post = new Post
			{
				AuthorId = authorId,
				Author = author,
				Text = text,
				DatePosted = DateTime.Now
			};

			var successful = await _postRepository.AddAsync(post);
			return successful;
		}
		// public IReadOnlyList<Post> GetPostsPage(List<Friend> friendsList)
		// {
		// 	foreach (Friend friend in friendsList)
		// 	{

		// 	}
		// }
    }
}