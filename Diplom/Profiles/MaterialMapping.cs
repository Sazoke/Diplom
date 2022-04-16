using System.Linq;
using AutoMapper;
using Infrastructure.Dtos.Material;
using Infrastructure.Models;

namespace Diplom.Profiles;

public class MaterialMapping : Profile
{
    public MaterialMapping()
    {
        CreateMap<MaterialDto, Material>();
        CreateMap<Material, MaterialDto>()
            .ForMember(m => m.Content, expression => expression.Ignore())
            .AfterMap((material, dto) =>
            {
                dto.Content = material.Content.Select(c => new ContentDto()
                {
                    IsFile = c.IsFile,
                    Text = c.Text
                }).ToList();
            });
        CreateMap<Material, MaterialProfilePreview>();
        CreateMap<Material, MaterialSearchPreview>()
            .ForMember(m => m.DateTime, expression => expression.MapFrom(f => f.CreatedAt));
    }
}