using Infrastructure.Dtos.Base;

namespace Infrastructure.Dtos.Material;

public class MaterialFilterResultDto : FilterResultDto
{
    public string Type { get; set; }
}