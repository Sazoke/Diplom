using Infrastructure.Dtos.Base;

namespace Infrastructure.Dtos.Material;

public class MaterialFilter : Filter
{
    public long? TypeId { get; set; }
}