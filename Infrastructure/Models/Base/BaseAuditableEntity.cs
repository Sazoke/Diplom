using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Models.Application;

namespace Infrastructure.Models.Base;

public class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    
    public string CreatedById { get; set; }
    
    [ForeignKey(nameof(CreatedById))]
    public ApplicationUser CreatedBy { get; set; }
}