using Infrastructure.Dtos.Tag;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface ITagService
{
    public Tag GetByIdAsync(long id);
    
    public IEnumerable<Tag> GetBySchoolArea(long schoolAreaId);

    public Task AddOrUpdate(TagDto tagDto);
}