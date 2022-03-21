using System.Threading.Tasks;
using Fakebook.Core.Entities;
using System.Reflection;
using System.Linq;
using Fakebook.Core.Interfaces;

namespace Fakebook.Core.Services
{
    public class LikeService<T> : ILikeService<T> where T : BaseEntity
    {
        private readonly IAsyncRepository<Like> _likeRepository;

		public LikeService(IAsyncRepository<Like> likeRepository)
		{
			_likeRepository = likeRepository;
		}

		public async Task<bool> LikeAsync(string postId, string userId)
		{
			var like = new Like
			{
				UserId = userId,
				PostId = postId
			};

			return await _likeRepository.AddAsync(like);
		}
		
		public async Task<bool> UnlikeAsync(string postId, string userId)
		{
			var like = (await _likeRepository.ListAllAsync()).Where(l => l.PostId == postId && l.UserId == userId).First();
			return await _likeRepository.DeleteAsync(like);
		}
		
		public async Task<bool> CheckIfUserLikesAsync(string postId, string userId)
		{
			var likes = await _likeRepository.ListAllAsync();
			return likes
				.Where(l => l.PostId == postId)
			 	.Where(l => l.UserId == userId)
				.Any();
		}
	}
}