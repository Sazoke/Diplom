using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Models.Base;

namespace Infrastructure.Models.Test;

public class Test : BaseAuditableEntity
{
    public string Name { get; set; }
    
    [Column(TypeName = "jsonb")]
    public List<Question> Questions { get; set; }
    
    public List<UserResult> Results { get; set; }
}