using System;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;

namespace Fakebook.Core.Services
{
    public class CommentService : ICommentService
    {
		private readonly IAsyncRepository<Comment> _commentRepository;

		public CommentService(IAsyncRepository<Comment> commentRepository)
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
			comment.Id = Guid.NewGuid().ToString();
			
			bool successful = await _commentRepository.AddAsync(comment);
			return successful;
		}

		public async Task<Comment> GetByIdAsync(string commentId)
		{
			return await _commentRepository.GetByIdAsync(commentId);
		}
    }
}