using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Models.Base;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Diplom.Dtos.Base;

public class ExtendedDto : BaseDto
{
    public string Description { get; set; }
    
    public List<IFormFile> Files { get; set; }

    public async Task CompleteFiles(TeacherComponent component, IBucket bucket)
    {
        /*Image = await bucket.ReadFileAsync(component.Image);
        foreach (var fileName in component.Files)
            Files.Add(await bucket.ReadFileAsync(fileName));*/
    }
}