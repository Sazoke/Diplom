using Infrastructure.Models;

namespace Infrastructure.Dtos.Material;

public class MaterialDto
{
    public long? Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Image { get; set; }
    
    public string Type { get; set; }
    
    public string TeacherId { get; set; }
    
    public List<Content> Content { get; set; }
}