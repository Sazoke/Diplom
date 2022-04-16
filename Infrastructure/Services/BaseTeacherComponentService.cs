using Infrastructure.Models.Base;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public abstract class BaseTeacherComponentService<T> where T : TeacherComponent
{
    private readonly BaseRepository<T> _repository;
    private readonly IBucket _bucket;

    public BaseTeacherComponentService(BaseRepository<T> repository, IBucket bucket)
    {
        _repository = repository;
        _bucket = bucket;
    }

    public async Task EditNameAsync(long id, string name)
    {
        var component = await _repository.GetById(id);
        component.Name = name;
        await _repository.Update(component);
        await _repository.SaveChangesAsync();
    }

    public async Task EditDescription(long id, string description)
    {
        var component = await _repository.GetById(id);
        component.Description = description;
        await _repository.Update(component);
        await _repository.SaveChangesAsync();
    }

    public async Task<string> EditImage(long id, IFormFile file)
    {
        var component = await _repository.GetById(id);
        component.Image = await _bucket.WriteFileAsync(file);
        await _repository.Update(component);
        await _repository.SaveChangesAsync();
        return component.Image;
    }

    public async Task<string> AddFile(long id, IFormFile file)
    {
        var component = await _repository.GetById(id);
        component.Files.Add(await _bucket.WriteFileAsync(file));
        await _repository.Update(component);
        await _repository.SaveChangesAsync();
        return component.Files.Last();
    }

    public async Task DeleteFile(long id, string fileName)
    {
        var component = await _repository.GetById(id);
        component.Files.Remove(fileName);
        await _repository.Update(component);
        await _repository.SaveChangesAsync();
    }
}