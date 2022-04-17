using AutoMapper;
using Infrastructure.Dtos.User;
using Infrastructure.Models.Application;

namespace Diplom.Profiles;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<ApplicationUser, UserProfileDto>()
            .ForMember(u => u.Activities, o => o.Ignore())
            .ForMember(u => u.Materials, o => o.Ignore())
            .ForMember(u => u.EducationalMaterials, o => o.Ignore());
    }
}