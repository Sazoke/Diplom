using Microsoft.AspNetCore.Http;

namespace Diplom.Dtos.User;

public class CreateUserDto
{
    public string Name { get; set; }
    
    public string Password { get; set; }
    
    public IFormFile Image { get; set; }
    
    public string Email { get; set; }
    
    public string Description { get; set; }
}