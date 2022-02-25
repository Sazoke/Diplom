using System;
using System.Collections.Generic;
using Diplom.Dtos.Base;
using Microsoft.AspNetCore.Http;

namespace Diplom.Dtos.Activity;

public class PreviewActivityDto : BaseDto
{
    public DateTime Time { get; set; }
}