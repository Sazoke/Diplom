using Infrastructure.Models;

namespace Infrastructure.Dtos.Material;

public class MaterialDto
{
    public long? Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Image { get; set; }
    
    public long TypeId { get; set; }

    public long? AreaId { get; set; }
    
    public HashSet<long> Tags { get; set; }
    
    public string TeacherId { get; set; }
    
    public List<Content> Content { get; set; }
}