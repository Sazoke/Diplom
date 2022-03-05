using Infrastructure.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models.Test;

public class Question : BaseAuditableEntity
{
    public string QuestionText { get; set; }
    
    public List<string> Options { get; set; }
    
    public string Answer { get; set; }

    public bool IsCorrectAnswer(string answer) => answer == Answer;
}