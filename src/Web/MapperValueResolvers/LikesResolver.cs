
// using AutoMapper;
// using Fakebook.Core.Entities;
// using Fakebook.Core.Interfaces;
// using Fakebook.Web.Models.ViewModels;

// namespace Fakebook.Web.MapperValueResolvers
// {
// 	public class LikesResolver<T, TProperty> : IValueResolver<T, T, bool> where T : BaseEntity
// 	{
// 		private readonly ILikeService<Post> _postLikeService;
// 		private readonly ILikeService<Comment> _commentLikeService;
// 			public LikesResolver(ILikeService<Post> postLikeService, ILikeService<Comment> commentLikeService)
// 		{
// 			_postLikeService = postLikeService;
// 			_commentLikeService = commentLikeService;
// 		}

// 		public bool Resolve(T src, T dest, bool destMember, ResolutionContext context)
// 		{
// 			var type = src.GetType().GetGenericTypeDefinition();
// 			if (type == typeof(Post))
// 			{
// 				PropertyInfo propInfo = src.GetType().GetProperty("User.Id");	
// 				var newvalue = src.GetType().GetProperty(property.Name).GetValue(userInput);
// 				return _postLikeService.CheckIfUserLikesAsync(src.Id, src.GetType().GetProperty(User.Id).GetValue(src.User.Id)).GetAwaiter().GetResult();
// 			}
// 		}
// 	}
// }