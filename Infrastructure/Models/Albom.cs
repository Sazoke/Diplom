using Infrastructure.Models.Base;

namespace Infrastructure.Models;

public class Albom : BaseAuditableEntity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public List<string> Images { get; set; }
}