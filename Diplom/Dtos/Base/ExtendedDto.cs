using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Diplom.Dtos.Base;

public class ExtendedDto : BaseDto
{
    public string Description { get; set; }
    
    public List<IFormFile> Files { get; set; }
}