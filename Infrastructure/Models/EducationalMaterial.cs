using Infrastructure.Models.Base;

namespace Infrastructure.Models;

public class EducationalMaterial : BaseAuditableEntity
{
    public string? Name { get; set; }
    
    public string? Image { get; set; }
}