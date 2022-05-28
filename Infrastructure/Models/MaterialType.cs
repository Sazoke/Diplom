using Infrastructure.Models.Base;

namespace Infrastructure.Models;

public class MaterialType : BaseAuditableEntity
{
    public string SingleTypeName { get; set; }
    
    public string MultipleTypeName { get; set; }
}