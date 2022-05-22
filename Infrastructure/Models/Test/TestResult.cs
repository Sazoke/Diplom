using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Models.Base;

namespace Infrastructure.Models.Test;

public class TestResult : BaseAuditableEntity
{
    public long TestId { get; set; }
    
    [ForeignKey(nameof(TestId))]
    public Test Test { get; set; }
    
    public string Username { get; set; }

    public int Percent { get; set; }
}