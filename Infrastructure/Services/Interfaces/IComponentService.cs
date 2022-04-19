using Infrastructure.Dtos.Base;
using Infrastructure.Models;
using Infrastructure.Models.Base;

namespace Infrastructure.Services.Interfaces;

public interface IComponentService<T> where T : TeacherComponent
{
    public T GetById(long id);

    public IEnumerable<T> GetByFilter(Filter filter);
    
    public Task RemoveAsync(long id);
}