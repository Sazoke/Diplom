using Microsoft.AspNetCore.Http;

namespace Infrastructure.Dtos.Base;

public class BaseDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    public IFormFile Image { get; set; }
}