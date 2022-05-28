using AutoMapper;
using Infrastructure.Dtos.Base;
using Infrastructure.Dtos.Material;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class MaterialService : BaseComponentService<Material>, IMaterialService
{
    private readonly TagRepository _tagRepository;
    private readonly BaseRepository<MaterialType> _typeRepository;

    public MaterialService(BaseRepository<Material> repository, TagRepository tagRepository, BaseRepository<MaterialType> typeRepository) : base(repository)
    {
        _tagRepository = tagRepository;
        _typeRepository = typeRepository;
    }

    public Material GetById(long id) =>
        Repository.GetById(id, q => q.Include(a => a.Tags)
            .Include(m => m.Type));

    public async Task AddOrUpdateAsync(MaterialEditDto materialDto)
    {
        var material = materialDto.Id is null ? new Material() : GetById(materialDto.Id.Value);
        UpdateMaterial(material, materialDto);
        if (materialDto.Id is null)
        {
            await Repository.AddAsync(material);
            material.Tags = _tagRepository.GetByIds(materialDto.Tags).ToList();
            await Repository.UpdateAsync(material);
        }
        else
            await Repository.UpdateAsync(material);
    }

    public IEnumerable<MaterialType> GetAllMaterialTypes() => _typeRepository.GetAll();
    public async Task AddMaterialType(string singleName, string multipleName)
    {
        await _typeRepository.AddAsync(new MaterialType()
        {
            SingleTypeName = singleName,
            MultipleTypeName = multipleName
        });
        await _typeRepository.SaveChangesAsync();
    }

    public async Task RemoveAsync(long id)
    {
        await Repository.RemoveAsync(id);
    }

    private void UpdateMaterial(Material material, MaterialEditDto materialDto)
    {
        material.Name = materialDto.Name;
        material.Description = materialDto.Description;
        material.Image = materialDto.Image;
        material.AreaId = materialDto.AreaId;
        material.TypeId = materialDto.TypeId;
        material.Content = materialDto.Content;
    }
}