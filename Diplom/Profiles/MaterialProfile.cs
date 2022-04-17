using AutoMapper;
using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Material;
using Infrastructure.Models;

namespace Diplom.Profiles;

public class MaterialProfile : Profile
{
    public MaterialProfile()
    {
        CreateMap<MaterialDto, Material>();
        CreateMap<Material, MaterialDto>();
        CreateMap<Material, MaterialProfilePreview>();
        CreateMap<Material, FilterResultDto>()
            .ForMember(m => m.Date, expression => expression.MapFrom(f => f.CreatedAt));
    }
}