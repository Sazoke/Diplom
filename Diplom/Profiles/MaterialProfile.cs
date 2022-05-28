using System.Linq;
using AutoMapper;
using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Material;
using Infrastructure.Models;

namespace Diplom.Profiles;

public class MaterialProfile : Profile
{
    public MaterialProfile()
    {
        CreateMap<Material, MaterialDto>()
            .ForMember(a => a.TeacherId, expression => expression.MapFrom(a => a.CreatedById))
            .ForMember(m => m.Tags, expression => expression.Ignore())
            .ForMember(m => m.TypeId, expression => expression.MapFrom(m => m.TypeId))
            .AfterMap(((material, dto) =>
            {
                dto.Tags = material.Tags.Select(t => t.Id).ToHashSet();
            }));
        CreateMap<Material, MaterialProfilePreview>()
            .ForMember(m => m.Type, expression => expression.MapFrom(m => m.Type.SingleTypeName));
        CreateMap<Material, FilterResultDto>()
            .ForMember(m => m.Date, expression => expression.MapFrom(f => f.CreatedAt))
            .ForMember(a => a.TeacherId, expression => expression.MapFrom(a => a.CreatedById));
    }
}