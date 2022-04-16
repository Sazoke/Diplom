using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Dtos.Activity;
using Infrastructure.Dtos.Base;
using Infrastructure.Models;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Diplom.Profiles;

public class ActivityProfile : Profile
{
    public ActivityProfile()
    {
        CreateMap<Activity, ActivityDto>();
        CreateMap<Activity, FilterResultDto>();
    }
}