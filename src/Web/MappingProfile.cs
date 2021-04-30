using AutoMapper;
using Fakebook.Core.Entities;
using Fakebook.Web.Models;
using Fakebook.Web.Models.ViewModels;

namespace Fakebook.Web
{
    public class MappingProfile : Profile
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
				.ForMember(m => m.Likes, o => o.MapFrom(src => src.Likes.Length));
		}
    }
}
