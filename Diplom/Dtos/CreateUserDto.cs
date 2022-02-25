using Diplom.Dtos.Base;

namespace Diplom.Dtos;

public class CreateUserDto : BaseDto
{
    public string UserName { get; set; }
    
    public string Image { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
    
    public string Description { get; set; }
}