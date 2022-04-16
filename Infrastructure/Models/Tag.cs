using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Models.Base;

namespace Infrastructure.Models;

public class Tag : BaseAuditableEntity
{
    public string Name { get; set; }
    
    public long SchoolAreaId { get; set; }
    
    [ForeignKey(nameof(SchoolAreaId))]
    public SchoolArea SchoolArea { get; set; }
    
    public List<Material> Materials { get; set; } = new ();
    
    public List<Activity> Activities { get; set; } = new ();
    
    public List<Test.Test> Tests { get; set; } = new ();
}