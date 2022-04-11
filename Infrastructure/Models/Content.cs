using Infrastructure.Models.Base;

namespace Infrastructure.Models;

public class Content : BaseAuditableEntity
{
    public bool IsFile { get; set; }
    
    public string Text { get; set; }
}