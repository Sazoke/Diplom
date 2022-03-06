using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Models.Application;

namespace Infrastructure.Models.Base;

public abstract class TeacherComponent : BaseAuditableEntity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string TeacherId { get; set; }
    
    [ForeignKey(nameof(TeacherId))]
    public ApplicationUser Teacher { get; set; }
    
    public string Image { get; set; }
    
    public List<string> Files { get; set; }
}