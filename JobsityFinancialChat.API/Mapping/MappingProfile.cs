using AutoMapper;
using JobsityFinancialChat.Domain.API.User;
using JobsityFinancialChat.Domain.Models.API.Chatroom;
using JobsityFinancialChat.Domain.Models.DB;
using System;

namespace JobsityFinancialChat.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, LoggedInUserDto>();

            CreateMap<RegisterRequestDto, ApplicationUser>()
               .ForMember(x => x.LastLoginDate, y => y.MapFrom(z => DateTime.UtcNow))
               .ForMember(x => x.UserName, y => y.MapFrom(z => z.Email))
               .ForMember(x => x.Active, y => y.MapFrom(z => true))
               .ForMember(x => x.PasswordHash, y => y.MapFrom(z => z.Password));

            CreateMap<ApplicationUser, ChatroomMemberModel>()
                 .ForMember(x => x.Email, y => y.MapFrom(z => z.Email));

            CreateMap<Chatroom, ChatroomModel>()
                 .ForMember(x => x.Members, y => y.MapFrom(z => z.Members));
        }
    }
}