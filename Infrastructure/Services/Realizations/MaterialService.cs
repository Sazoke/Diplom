using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Material;
using Infrastructure.Models;
using Infrastructure.Models.Application;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class MaterialService : BaseComponentService<Material>, IMaterialService
{
    private readonly BaseRepository<Content> _contentRepository;

    public MaterialService(BaseRepository<Material> repository, ApplicationContext applicationContext,
        TagRepository tagRepository, BaseRepository<Content> contentRepository) : base(repository, applicationContext,
        tagRepository)
    {
        _contentRepository = contentRepository;
    }

    public async Task<Material> GetByIdAsync(long id) => 
        await Repository.GetById(id, q => q.Include(m => m.Content));

    public async Task<IEnumerable<Material>> GetByFilterAsync(FilterDto filterDto)
    {
        var materials = await Repository.GetAll(q => q.Include(c => c.Tags)
            .Where(c => (!filterDto.SchoolArea.HasValue || filterDto.SchoolArea == c.AreaId) &&
                        (!filterDto.Tags.Any() || c.Tags.All(t => filterDto.Tags.Contains(t.Id))) &&
                        (filterDto.TeacherId != null || filterDto.TeacherId == c.TeacherId)).Skip(filterDto.Skip)
            .Take(filterDto.PageSize));
        return materials;
    }

    public async Task EditTypeAsync(long id, string type)
    {
        var material = await GetByIdAsync(id);
        material.Type = type;
        await Repository.UpdateAsync(material);
    }

    public async Task<Content> AddFragmentAsync(long id)
    {
        var material = await GetByIdAsync(id);
        var result = new Content();
        material.Content.Add(result);
        await Repository.UpdateAsync(material);
        return result;
    }

    public async Task EditFragmentAsync(FragmentDto fragmentDto)
    {
        var content = await _contentRepository.GetById(fragmentDto.Id.Value);
        content.Text = fragmentDto.Content;
        await _contentRepository.UpdateAsync(content);
    }

    public async Task RemoveFragmentAsync(long id) => await _contentRepository.Delete(id);
}