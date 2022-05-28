using System.Linq;
using AutoMapper;
using Infrastructure.Dtos.Test;
using Infrastructure.Models.Test;

namespace Diplom.Profiles;

public class TestProfile : Profile
{
    public TestProfile()
    {
        CreateMap<Test, TestDto>().ForMember(t => t.Results, 
                expression => expression.Ignore())
            .AfterMap(((test, dto) =>
            {
                dto.Results = test.Results.Select(r => new TestResultDto()
                {
                    TestId = r.TestId,
                    Percent = r.Percent,
                    Username = r.Username
                }).ToList();
            }));
        CreateMap<Test, TestPreviewDto>();
    }
}