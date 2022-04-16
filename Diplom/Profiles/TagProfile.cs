using AutoMapper;
using Infrastructure.Dtos.Tag;
using Infrastructure.Models;

namespace Diplom.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>();
    }
}