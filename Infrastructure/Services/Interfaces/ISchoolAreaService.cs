using Infrastructure.Dtos.SchoolArea;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface ISchoolAreaService
{
    public Task<SchoolArea> GetByIdAsync(long id);

    public Task<IEnumerable<SchoolArea>> GetAllAsync();

    public Task AddOrUpdate(SchoolAreaDto schoolAreaDto);
}