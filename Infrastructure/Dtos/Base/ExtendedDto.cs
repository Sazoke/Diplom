using Microsoft.AspNetCore.Http;

namespace Infrastructure.Dtos.Base;

public class ExtendedDto : BaseDto
{
    public string Description { get; set; }
    
    public List<IFormFile> Files { get; set; }
}