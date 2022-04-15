using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Material;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface IMaterialService : IComponentService<Material>
{
    public Task EditTypeAsync(long id, string type);
    public Task<Content> AddFragmentAsync(long id);

    public Task EditFragmentAsync(FragmentDto fragmentDto);

    public Task RemoveFragmentAsync(long id);
}