using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Models.Application;

namespace Infrastructure.Models.Base;

public abstract class TeacherComponent : BaseAuditableEntity
{
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public string? Image { get; set; }

    public long? AreaId { get; set; }
    
    [ForeignKey(nameof(AreaId))]
    public SchoolArea Area { get; set; }
    
    public List<Tag>? Tags { get; set; }
}