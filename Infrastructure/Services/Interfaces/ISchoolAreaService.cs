using Infrastructure.Dtos.SchoolArea;
using Infrastructure.Models;

namespace Infrastructure.Services.Interfaces;

public interface ISchoolAreaService
{
    public SchoolArea GetById(long id);

    public IEnumerable<SchoolArea> GetAll();

    public Task AddOrUpdate(SchoolAreaDto schoolAreaDto);
}