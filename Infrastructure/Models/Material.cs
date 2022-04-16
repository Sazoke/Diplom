using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Models.Base;

namespace Infrastructure.Models;

public class Material : TeacherComponent
{
    public string Type { get; set; }
    
    [Column(TypeName = "jsonb")]
    public List<Content> Content { get; set; }
}