using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services.Realizations;

public class TagService : ITagService
{
    private readonly TagRepository _repository;

    public TagService(TagRepository repository)
    {
        _repository = repository;
    }

    public async Task<Tag> GetByIdAsync(long id) => await _repository.GetById(id);

    public IEnumerable<Tag> GetBySchoolArea(long schoolAreaId) =>
        _repository.GetBySchoolArea(schoolAreaId);

    public async Task<Tag> CreateAsync()
    {
        var result = await _repository.AddAsync(new Tag());
        return result;
    }

    public async Task<bool> TryEditNameAsync(long id, string name)
    {
        try
        {
            var tag = await _repository.GetById(id);
            tag.Name = name;
            await _repository.UpdateAsync(tag);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public async Task<bool> TryEditSchoolAreaAsync(long id, long schoolAreaId)
    {
        try
        {
            var tag = await _repository.GetById(id);
            tag.SchoolAreaId = schoolAreaId;
            await _repository.UpdateAsync(tag);
            return true;
        }
        catch
        {
            return false;
        }
    }
}