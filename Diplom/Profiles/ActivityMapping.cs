using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Diplom.Dtos.Activity;
using Infrastructure.Models;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Diplom.Profiles;

public class ActivityMapping : Profile
{
    public ActivityMapping()
    {
        CreateMap<Activity, PreviewActivityDto>();
        CreateMap<Activity, ActivityDto>();
        CreateMap<ActivityDto, Activity>();
    }
}