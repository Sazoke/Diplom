using Infrastructure.Dtos.Tag;
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

    public async Task<IEnumerable<Tag>> GetBySchoolArea(long schoolAreaId) =>
        await _repository.GetBySchoolArea(schoolAreaId);

    public async Task AddOrUpdate(TagDto tagDto)
    {
        var tag = tagDto.Id is null ? new Tag() : await GetByIdAsync(tagDto.Id.Value);
        tag.Name = tagDto.Name;
        tag.SchoolAreaId = tagDto.SchoolAreaId;
        if (tagDto.Id is null)
            await _repository.AddAsync(tag);
        else
            await _repository.UpdateAsync(tag);
    }
}