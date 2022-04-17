using Infrastructure.Dtos.Base;
using Infrastructure.Models.Base;

namespace Infrastructure.Services.Interfaces;

public interface IComponentService<T> where T : TeacherComponent
{
    public Task<T> GetByIdAsync(long id);

    public Task<IEnumerable<T>> GetByFilterAsync(FilterDto filterDto);
    
    public Task RemoveAsync(long id);
}