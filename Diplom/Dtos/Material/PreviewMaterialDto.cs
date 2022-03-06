using Diplom.Dtos.Base;
using Microsoft.AspNetCore.Http;

namespace Diplom.Dtos.Material;

public class PreviewMaterialDto : BaseDto
{
    public string MaterialType { get; set; }
}