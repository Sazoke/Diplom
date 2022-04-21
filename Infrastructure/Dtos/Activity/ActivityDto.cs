namespace Infrastructure.Dtos.Activity;

public class ActivityDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Image { get; set; }
    
    public string Description { get; set; }

    public long? AreaId { get; set; }
    
    public HashSet<long> Tags { get; set; }
    
    public string TeacherId { get; set; }
    
    public DateTime Date { get; set; }
}