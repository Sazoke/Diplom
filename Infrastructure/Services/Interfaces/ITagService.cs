using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface ITagService
{
    public Task<Tag> GetByIdAsync(long id);
    
    public IEnumerable<Tag> GetBySchoolArea(long schoolAreaId);

    public Task<Tag> CreateAsync();
    
    public Task<bool> TryEditNameAsync(long id, string name);

    public Task<bool> TryEditSchoolAreaAsync(long id, long schoolAreaId);
}