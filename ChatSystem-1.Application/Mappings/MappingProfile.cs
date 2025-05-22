using AutoMapper;
using ChatSystem_1.Application.DTOs;
using ChatSystem_1.Domain.Entities.Models;

namespace ChatSystem_1.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Username))
                .ForMember(u => u.FirstName, opt => opt.MapFrom(x => x.Firstname))
                .ForMember(u => u.LastName, opt => opt.MapFrom(x => x.Lastname))
                .ForMember(u => u.Email, opt => opt.MapFrom(x => x.Email))
                .ForMember(u => u.MatricNumber, opt => opt.MapFrom(x => x.MatricNumber));
        }
    }
}