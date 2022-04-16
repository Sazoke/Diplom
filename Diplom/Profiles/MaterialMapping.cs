using System.Linq;
using AutoMapper;
using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Material;
using Infrastructure.Models;

namespace Diplom.Profiles;

public class MaterialMapping : Profile
{
    public MaterialMapping()
    {
        CreateMap<MaterialDto, Material>();
        CreateMap<Material, MaterialDto>();
        CreateMap<Material, MaterialProfilePreview>();
        CreateMap<Material, FilterResultDto>()
            .ForMember(m => m.DateTime, expression => expression.MapFrom(f => f.CreatedAt));
    }
}