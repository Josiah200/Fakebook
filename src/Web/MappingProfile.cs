using System.Linq;
using AutoMapper;
using Fakebook.Core.Entities;
using Fakebook.Core.Interfaces;
using Fakebook.Web.Models;
using Fakebook.Web.Models.ViewModels;

namespace Fakebook.Web
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
			CreateMap<User, UserViewModel>();
			CreateMap<UserProfileUpdateModel, User>();

			CreateMap<Post, PostViewModel>()
				.ForMember(m => m.FirstName, o => o.MapFrom(src => src.User.FirstName))
				.ForMember(m => m.LastName, o => o.MapFrom(src => src.User.LastName))
				.ForMember(m => m.UserPublicId, o => o.MapFrom(src => src.User.PublicId))
				.ForMember(m => m.ProfilePicture, o => o.MapFrom(src => src.User.ProfilePicture))
				.ForMember(m => m.Comments, o => o.MapFrom(src => src.Comments.Where(c => !c.IsReply)))
				.ForMember(m => m.Likes, o => o.MapFrom(src => src.Likes.Count));

			CreateMap<Comment, CommentViewModel>()
				.IncludeAllDerived()
				.ForMember(m => m.Author, o => o.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
				.ForMember(m => m.AuthorPublicId, o => o.MapFrom(src => src.User.PublicId))
				.ForMember(m => m.ProfilePicture, o => o.MapFrom(src => src.User.ProfilePicture))
				.ForMember(m => m.Likes, o => o.MapFrom(src => src.Likes.Count))
				.ForMember(m => m.ParentCommentId, o => o.MapFrom(src => src.ParentCommentId))
				.ForMember(m => m.PostId, o => o.MapFrom(src => src.PostId));

			CreateMap<Comment, ParentCommentViewModel>()
				.ForMember(m => m.Replies, o => o.MapFrom(src => src.Replies));

			CreateMap<Friendship, FriendViewModel>()
				.ForMember(m => m.PublicId, o => o.MapFrom(src => src.Friend.PublicId))
				.ForMember(m => m.FirstName, o => o.MapFrom(src => src.Friend.FirstName))
				.ForMember(m => m.LastName, o => o.MapFrom(src => src.Friend.LastName))
				.ForMember(m => m.ProfilePicture, o => o.MapFrom(src => src.Friend.ProfilePicture));
		}
    }
}
