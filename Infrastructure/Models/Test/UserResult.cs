using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.Models.Application;
using Infrastructure.Models.Base;

namespace Infrastructure.Models.Test;

public class UserResult : BaseAuditableEntity
{
    public string UserName { get; set; }

    public int Percent { get; set; }
}