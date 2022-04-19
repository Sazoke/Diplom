using Infrastructure.Models.Test;

namespace Infrastructure.Dtos.Test;

public class TestEditDto
{
    public long? Id { get; set; }
    
    public string Name { get; set; }
    
    public List<Question> Questions { get; set; }
}