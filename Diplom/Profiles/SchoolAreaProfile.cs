using AutoMapper;
using Infrastructure.Dtos.SchoolArea;
using Infrastructure.Models;

namespace Diplom.Profiles;

public class SchoolAreaProfile : Profile
{
    public SchoolAreaProfile()
    {
        CreateMap<SchoolArea, SchoolAreaDto>();
    }
}