using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Test;
using Infrastructure.Models.Test;

namespace Infrastructure.Services.Interfaces;

public interface ITestService
{
    public IEnumerable<Test> GetByFilter(Filter filter);

    public Test GetById(long id);

    public Task AddOrUpdateAsync(TestEditDto testEditDto);

    public Task RemoveAsync(long id);

    public Task AddTestResult(long testId, string username, int percent);
}