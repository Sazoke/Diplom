using Infrastructure.Models.Test;

namespace Infrastructure.Dtos.Test;

public class TestDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public List<Question> Questions { get; set; }
    
    public List<TestResultDto> Results { get; set; }
}