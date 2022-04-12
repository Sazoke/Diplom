namespace Infrastructure.Dtos.Base;

public class FilterDto
{
    public int Page { get; set; }
    
    public int PageSize { get; set; }

    public HashSet<long> Tags { get; set; } = new();
    
    public long? SchoolArea { get; set; }
    
    public string? TeacherId { get; set; }

    public int Skip => (Page - 1) * PageSize;
}