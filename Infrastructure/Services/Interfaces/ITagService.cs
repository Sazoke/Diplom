using Infrastructure.Dtos.Tag;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface ITagService
{
    public Task<Tag> GetByIdAsync(long id);
    
    public Task<IEnumerable<Tag>> GetBySchoolArea(long schoolAreaId);

    public Task AddOrUpdate(TagDto tagDto);
}