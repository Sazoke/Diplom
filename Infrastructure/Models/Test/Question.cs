using Infrastructure.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models.Test;

public class Question
{
    public string Text { get; set; }
    
    public List<Answer> Answers { get; set; }
}