using Infrastructure.Dtos.Activity;
using Infrastructure.Dtos.Material;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Dtos.User;

public class UserProfileDto
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }
    
    public string Image { get; set; }
    
    public List<MaterialProfilePreview> Materials { get; set; }
    
    public List<ActivityProfilePreview> Activities { get; set; }
}