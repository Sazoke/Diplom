namespace Infrastructure.Dtos.Base;

public class Filter
{
    public int Page { get; set; }
    
    public int PageSize { get; set; }
    
    public string? Text { get; set; }

    public HashSet<long>? Tags { get; set; }
    
    public long? SchoolArea { get; set; }
    
    public string? TeacherId { get; set; }
    
    public DateTime? DateTime { get; set; }

    public int Skip => (Page - 1) * PageSize;
}