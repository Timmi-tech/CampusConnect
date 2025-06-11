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

            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Matricnumber, opt => opt.MapFrom(src => src.MatricNumber))
                .ForMember(dest => dest.ProfileImageUrl, opt => opt.MapFrom(src => src.ProfileImageUrl))
                .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.Email));


            CreateMap<UserUpdateProfileDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<CreatePostDto, Post>();
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.ImageUrls,
                    opt => opt.MapFrom(src => src.PostImages.Select(pi => pi.ImageUrl).ToList()));
            CreateMap<UpdatePostDto, Post>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        }
    }
}