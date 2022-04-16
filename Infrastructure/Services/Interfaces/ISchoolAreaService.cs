using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface ISchoolAreaService
{
    public Task<SchoolArea> GetByIdAsync(long id);

    public Task<IEnumerable<SchoolArea>> GetAllAsync();

    public Task<SchoolArea> CreateAsync();
    
    public Task<bool> TryEditNameAsync(long id, string name);
}