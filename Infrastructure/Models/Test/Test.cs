using Infrastructure.Models.Base;

namespace Infrastructure.Models.Test;

public class Test : BaseAuditableEntity
{
    
    public List<Question> Questions { get; set; }
    
    public List<UserResult> Results { get; set; }
}