using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Material;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface IMaterialService
{
    public Task<Material> GetByIdAsync(long id);

    public Task<IEnumerable<Material>> GetByFilterAsync(FilterDto filterDto);

    public Task<Material> CreateAsync();

    public Task EditNameAsync(long id, string name);
    
    public Task EditTypeAsync(long id, string type);

    public Task EditDescriptionAsync(long id, string description);

    public Task EditSchoolAreaAsync(long id, long areaId);

    public Task EditTagsAsync(long id, List<long> tagIds);

    public Task EditImageAsync(long id, string file);

    public Task<Content> AddFragmentAsync(long id);

    public Task EditFragmentAsync(FragmentDto fragmentDto);

    public Task RemoveFragmentAsync(long id);
}