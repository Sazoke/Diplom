using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Material;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface IMaterialService
{
    public Task<Material> GetByIdAsync(long id);

    public Task<IEnumerable<Material>> GetByFilterAsync(FilterDto filter);
    
    public Task AddOrUpdateAsync(MaterialEditDto materialDto);

    public Task RemoveAsync(long id);
}