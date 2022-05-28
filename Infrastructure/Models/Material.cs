using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Models.Base;

namespace Infrastructure.Models;

public class Material : TeacherComponent
{
    public long TypeId { get; set; }
    
    [ForeignKey(nameof(TypeId))]
    public MaterialType Type { get; set; }
    
    [Column(TypeName = "jsonb")]
    public List<Content> Content { get; set; }
}