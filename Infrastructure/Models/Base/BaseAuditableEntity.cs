using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Models.Application;

namespace Infrastructure.Models.Base;

public class BaseAuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }
}