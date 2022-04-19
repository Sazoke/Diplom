using Infrastructure.Dtos.EducationalMaterial;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface IEducationalMaterialService
{
    public EducationalMaterial GetById(long id);
    
    public Task RemoveAsync(long id);

    public Task AddOrUpdateAsync(EducationalMaterialEditDto editDto);
}