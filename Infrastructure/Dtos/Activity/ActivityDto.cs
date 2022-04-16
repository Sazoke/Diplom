using Infrastructure.Dtos.Base;

namespace Infrastructure.Dtos.Activity;

public class ActivityDto : ExtendedDto
{
    public long? Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    
    
    public DateTime Time { get; set; }
}