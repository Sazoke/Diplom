using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Test;
using Infrastructure.Models.Test;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class TestService : ITestService
{
    private readonly TestRepository _testRepository;

    public TestService(TestRepository testRepository)
    {
        _testRepository = testRepository;
    }

    public IEnumerable<Test> GetByFilter(Filter filter)
    {
        if (filter.TeacherId is null)
            throw new NullReferenceException();
        return _testRepository.GetByTeacherId(filter.TeacherId, filter.Page, filter.PageSize);
    }

    public Test GetById(long id) => 
        _testRepository.GetById(id, q => q.Include(t => t.Results));

    public async Task AddOrUpdateAsync(TestEditDto testEditDto)
    {
        var test = testEditDto.Id is null ? new Test() : GetById(testEditDto.Id.Value);
        test.Name = testEditDto.Name;
        test.Questions = testEditDto.Questions;
        if (testEditDto.Id is null)
            await _testRepository.AddAsync(test);
        else
            await _testRepository.UpdateAsync(test);
    }

    public async Task RemoveAsync(long id)
    {
        await _testRepository.RemoveAsync(id);
    }

    public async Task AddTestResult(long testId, string username, int percent)
    {
        var test = _testRepository.GetById(testId, q => q.Include(t => t.Results));
        var result = new TestResult()
        {
            CreatedById = test.CreatedById,
            CreatedAt = DateTime.UtcNow,
            Percent = percent,
            Username = username
        };
        test.Results.Add(result);
        await _testRepository.SaveChangesAsync();
    }
}