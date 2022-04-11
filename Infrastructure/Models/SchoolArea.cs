using Infrastructure.Models.Base;

namespace Infrastructure.Models;

public class SchoolArea : BaseEntity
{
    public string Name { get; set; }
    
    public List<Tag> Tags { get; set; }
}