using Infrastructure.Dtos.SchoolArea;
using Infrastructure.Dtos.Tag;

namespace Infrastructure.Dtos.Base;

public class ExtendedDto : BaseDto
{
    public string Image { get; set; }
    
    public string Description { get; set; }
    
    public List<TagDto> Tags { get; set; }

    public SchoolAreaDto SchoolArea { get; set; }
}