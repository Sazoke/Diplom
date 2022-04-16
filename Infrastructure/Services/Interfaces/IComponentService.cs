using Infrastructure.Dtos.Base;
using Infrastructure.Models.Base;

namespace Infrastructure.Services.Interfaces;

public interface IComponentService<T> where T : TeacherComponent
{
    public Task<T> GetByIdAsync(long id);

    public Task<IEnumerable<T>> GetByFilterAsync(FilterDto filterDto);

    public Task<T> CreateAsync();

    public Task EditNameAsync(long id, string name);

    public Task EditDescriptionAsync(long id, string description);

    public Task EditSchoolAreaAsync(long id, long areaId);

    public Task EditTagsAsync(long id, List<long> tagIds);

    public Task EditImageAsync(long id, string file);
}