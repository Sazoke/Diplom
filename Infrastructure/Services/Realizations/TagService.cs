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

    public Tag GetByIdAsync(long id) => _repository.GetById(id);

    public IEnumerable<Tag> GetBySchoolArea(long schoolAreaId) =>
        _repository.GetBySchoolArea(schoolAreaId);

    public async Task AddOrUpdate(TagDto tagDto)
    {
        var tag = tagDto.Id is null ? new Tag() : GetByIdAsync(tagDto.Id.Value);
        tag.Name = tagDto.Name;
        tag.SchoolAreaId = tagDto.SchoolAreaId;
        if (tagDto.Id is null)
            await _repository.AddAsync(tag);
        else
            await _repository.UpdateAsync(tag);
    }
}