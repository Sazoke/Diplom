using Infrastructure.Dtos.Material;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface IMaterialService : IComponentService<Material>
{
    public Task AddOrUpdateAsync(MaterialEditDto materialDto);

    public IEnumerable<MaterialType> GetAllMaterialTypes();

    public Task AddMaterialType(string singleName, string multipleName);
}