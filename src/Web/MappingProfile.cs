using AutoMapper;
using Fakebook.Core.Entities;
using Fakebook.Web.Models;

namespace Fakebook.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
			CreateMap<User, UserModel>();
			
            CreateMap<Post, PostModel>()
				.ForMember(m => m.FirstName, o => o.MapFrom(src => src.User.FirstName))
				.ForMember(m => m.LastName, o => o.MapFrom(src => src.User.LastName))
				.ForMember(m => m.UserPublicId, o => o.MapFrom(src => src.User.PublicId));	
		}
    }
}
