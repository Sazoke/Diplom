using Microsoft.AspNetCore.Http;

namespace Infrastructure.Dtos.Base;

public class BaseDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    public string Image { get; set; }
}