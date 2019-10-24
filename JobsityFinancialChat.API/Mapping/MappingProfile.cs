using AutoMapper;
using JobsityFinancialChat.Domain.API.User;
using JobsityFinancialChat.Domain.Models.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        }
    }
}