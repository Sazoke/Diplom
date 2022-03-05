using AutoMapper;
using Diplom.Dtos.Material;
using Infrastructure.Models;

namespace Diplom.Profiles;

public class MaterialMapping : Profile
{
    public MaterialMapping()
    {
        CreateMap<Material, PreviewMaterialDto>();
    }
}