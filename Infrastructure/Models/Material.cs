using Infrastructure.Models.Base;
using Infrastructure.Models.Enums;

namespace Infrastructure.Models;

public class Material : TeacherComponent
{
    public string Type { get; set; }
    public List<Content> Content { get; set; }
}