using AutoMapper;
using Diplom.Dtos;
using Diplom.Dtos.User;
using Infrastructure.Models.Application;

namespace Diplom.Profiles;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<CreateUserDto, ApplicationUser>()
            .ForMember(u => u.Image, o => o.Ignore());
        CreateMap<ApplicationUser, UserProfileDto>()
            .ForMember(u => u.Image, o => o.Ignore())
            .ForMember(u => u.Activities, o => o.Ignore())
            .ForMember(u => u.Materials, o => o.Ignore());
    }
}