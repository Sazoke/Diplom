namespace Infrastructure.Dtos.Activity;

public class ActivityEditDto
{
    public long? Id { get; set; }
    
    public string Name { get; set; }
    
    public string Image { get; set; }
    
    public string Description { get; set; }
    
    public string DateTime { get; set; }
    
    public long? AreaId { get; set; }
    
    public HashSet<long> Tags { get; set; }
}