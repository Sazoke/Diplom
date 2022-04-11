namespace Infrastructure.Models;

public class FilterInfo
{
    public long SchoolAreaId { get; set; }
    public HashSet<string> Words { get; set; }
    public HashSet<long> Tags { get; set; }
}