using AutoMapper;
using Infrastructure.Dtos.Material;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class MaterialService : BaseComponentService<Material>, IMaterialService
{
    private readonly TagRepository _tagRepository;
    public MaterialService(BaseRepository<Material> repository, TagRepository tagRepository) : base(repository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<Material> GetByIdAsync(long id) =>
        await Repository.GetById(id, q => q.Include(a => a.Tags));

    public async Task AddOrUpdateAsync(MaterialEditDto materialDto)
    {
        var material = materialDto.Id is null ? new Material() : await GetByIdAsync(materialDto.Id.Value);
        await UpdateMaterial(material, materialDto);
        if (materialDto.Id is null)
        {
            await Repository.AddAsync(material);
            material.Tags = (await _tagRepository.GetByIds(materialDto.Tags)).ToList();
            await Repository.UpdateAsync(material);
        }
        else
            await Repository.UpdateAsync(material);
    }

    public async Task RemoveAsync(long id)
    {
        await Repository.Delete(id);
    }

    private async Task UpdateMaterial(Material material, MaterialEditDto materialDto)
    {
        material.Name = materialDto.Name;
        material.Description = materialDto.Description;
        material.Image = materialDto.Image;
        material.AreaId = materialDto.AreaId;
        material.Type = materialDto.Type;
        material.Content = materialDto.Content;
    }  
}