using Infrastructure.Dtos.EducationalMaterial;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services.Realizations;

public class EducationalMaterialService : IEducationalMaterialService
{
    private readonly BaseRepository<EducationalMaterial> _repository;

    public EducationalMaterialService(BaseRepository<EducationalMaterial> repository)
    {
        _repository = repository;
    }

    public async Task<EducationalMaterial> GetByIdAsync(long id) => await _repository.GetById(id);

    public async Task RemoveAsync(long id) => await _repository.Delete(id);

    public async Task AddOrUpdateAsync(EducationalMaterialEditDto editDto)
    {
        var material = editDto.Id is null ? new EducationalMaterial() : await GetByIdAsync(editDto.Id.Value);
        material.Image = editDto.Image;
        material.Name = editDto.Name;
        if (editDto.Id is null)
            await _repository.AddAsync(material);
        else
            await _repository.UpdateAsync(material);
    }
}