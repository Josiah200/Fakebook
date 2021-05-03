using System;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;

namespace Fakebook.Core.Services
{
    public class CommentService : ICommentService
    {
		private readonly ICommentRepository _commentRepository;

		public CommentService(ICommentRepository commentRepository)
		{
			_commentRepository = commentRepository;
		}
		
        public async Task<bool> SaveCommentAsync(Comment comment)
		{
			if (comment is null)
			{
				throw new ArgumentNullException(nameof(comment));
			}
			comment.DatePosted = DateTime.UtcNow;
			
			bool successful = await _commentRepository.AddAsync(comment);
			return successful;
		}

		public async Task<bool> LikeCommentAsync(string commentId, string userId)
		{
			var comment = await _commentRepository.GetByIdAsync(commentId);
			comment.Likes = comment.Likes.Concat(new string[] {userId}).ToArray();
			return await _commentRepository.UpdateAsync(comment);
		}

		public async Task<bool> UnlikeCommentAsync(string commentId, string userId)
		{
			var comment = await _commentRepository.GetByIdAsync(commentId);
			comment.Likes = comment.Likes.Where(l => l != userId).ToArray();
			return await _commentRepository.UpdateAsync(comment);
		}

		public async Task<bool> CheckIfUserLikesCommentAsync(string commentId, string userId)
		{
			var comment = await _commentRepository.GetByIdAsync(commentId);
			if (comment.Likes.Contains(userId))
			{
				return true;
			}
			return false;
		}
    }
}