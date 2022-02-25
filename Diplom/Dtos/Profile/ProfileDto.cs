using System.Collections.Generic;
using Diplom.Dtos.Activity;
using Diplom.Dtos.Base;
using Diplom.Dtos.Material;

namespace Diplom.Dtos.Profile;

public class ProfileDto : ExtendedDto
{
    public List<PreviewActivityDto> Activities { get; set; }
    
    public List<PreviewMaterialDto> Materials { get; set; }
}