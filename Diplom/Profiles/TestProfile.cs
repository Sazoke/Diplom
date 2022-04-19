using AutoMapper;
using Infrastructure.Dtos.Test;
using Infrastructure.Models.Test;

namespace Diplom.Profiles;

public class TestProfile : Profile
{
    public TestProfile()
    {
        CreateMap<Test, TestDto>();
        CreateMap<Test, TestPreviewDto>();
    }
}