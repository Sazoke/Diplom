using Infrastructure.Models;
using Infrastructure.Models.Application;
using Infrastructure.Models.Base;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public abstract class BaseComponentService<T> where T : TeacherComponent
{
    protected readonly BaseRepository<T> Repository;
    private readonly TagRepository _tagRepository;
    private readonly ApplicationContext _applicationContext;

    protected BaseComponentService(BaseRepository<T> repository, ApplicationContext applicationContext, TagRepository tagRepository)
    {
        Repository = repository;
        _applicationContext = applicationContext;
        _tagRepository = tagRepository;
    }

    public async Task<T> CreateAsync()
    {
        var type = typeof(T);
        var constructor = type.GetConstructor(Array.Empty<Type>());
        var result = constructor?.Invoke(Array.Empty<object>()) as T;
        result.TeacherId = _applicationContext.CurrentUserId;
        await Repository.AddAsync(result);
        return result;
    }

    public async Task EditNameAsync(long id, string name)
    {
        var component = await Repository.GetById(id);
        component.Name = name;
        await Repository.UpdateAsync(component);
    }

    public async Task EditDescriptionAsync(long id, string description)
    {
        var component = await Repository.GetById(id);
        component.Description = description;
        await Repository.UpdateAsync(component);
    }

    public async Task EditImageAsync(long id, string file)
    {
        var component = await Repository.GetById(id);
        component.Image = file;
        await Repository.UpdateAsync(component);
    }

    public async Task EditSchoolAreaAsync(long id, long areaId)
    {
        var component = await Repository.GetById(id);
        component.AreaId = areaId;
        component.Tags = new List<Tag>();
        await Repository.UpdateAsync(component);
    }
    
    public async Task EditTagsAsync(long id, List<long> tagIds)
    {
        var component = await Repository.GetById(id);
        var tags = await _tagRepository.GetByIds(tagIds);
        component.Tags = tags.ToList();
        await Repository.UpdateAsync(component);
    }
}