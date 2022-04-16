using Infrastructure.Dtos.Base;

namespace Infrastructure.Dtos.Material;

public class MaterialDto : ExtendedDto
{
    public string MaterialType { get; set; }
    
    public List<FragmentDto> Content { get; set; }
}