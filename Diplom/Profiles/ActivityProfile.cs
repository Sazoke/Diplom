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
        CreateMap<Activity, ActivityDto>()
            .ForMember(a => a.TeacherId, expression => expression.MapFrom(a => a.CreatedById))
            .ForMember(m => m.Tags, expression => expression.Ignore())
            .AfterMap(((material, dto) =>
            {
                dto.Tags = material.Tags.Select(t => t.Id).ToHashSet();
            }));;
        CreateMap<Activity, ActivityProfilePreview>()
            .ForMember(a => a.DateTime, expression => expression.MapFrom(a => a.Date));
        CreateMap<Activity, FilterResultDto>()
            .ForMember(a => a.TeacherId, expression => expression.MapFrom(a => a.CreatedById));
    }
}