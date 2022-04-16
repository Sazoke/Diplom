using Infrastructure.Dtos.SchoolArea;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Realizations;

public class SchoolAreaService : ISchoolAreaService
{
    private BaseRepository<SchoolArea> _repository;

    public SchoolAreaService(BaseRepository<SchoolArea> repository)
    {
        _repository = repository;
    }

    public async Task<SchoolArea> GetByIdAsync(long id) => 
        await _repository.GetById(id, q => q.Include(s => s.Tags));

    public async Task<IEnumerable<SchoolArea>> GetAllAsync() =>
        await _repository.GetAll(q => q.Include(s => s.Tags));

    public async Task AddOrUpdate(SchoolAreaDto schoolAreaDto)
    {
        var schoolArea = schoolAreaDto.Id is null ? new SchoolArea() : await GetByIdAsync(schoolAreaDto.Id.Value);
        schoolArea.Name = schoolAreaDto.Name;
        if (schoolAreaDto.Id is null)
            await _repository.AddAsync(schoolArea);
        else
            await _repository.UpdateAsync(schoolArea);
    }
}