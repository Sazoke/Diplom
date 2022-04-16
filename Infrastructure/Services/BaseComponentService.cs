using Infrastructure.Dtos.Base;
using Infrastructure.Models;
using Infrastructure.Models.Application;
using Infrastructure.Models.Base;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public abstract class BaseComponentService<T> where T : TeacherComponent
{
    protected readonly BaseRepository<T> Repository;

    protected BaseComponentService(BaseRepository<T> repository)
    {
        Repository = repository;
    }

    public async Task<IEnumerable<T>> GetByFilterAsync(FilterDto filterDto)
    {
        var components = await Repository.GetAll(q =>
        {
            if (filterDto.Tags is not null)
                q = q.Include(c => c.Tags)
                    .Where(c => c.Tags.All(t => filterDto.Tags.Contains(t.Id)));
            if (filterDto.SchoolArea is not null)
                q = q.Where(c => filterDto.SchoolArea == c.AreaId);
            if (filterDto.DateTime is not null)
                q = q.Where(c => c.CreatedAt >= filterDto.DateTime);
            if (filterDto.TeacherId is not null)
                q = q.Where(c => c.CreatedById == filterDto.TeacherId);
            return q.Skip(filterDto.Skip)
                .Take(filterDto.PageSize);
        });
        return components;
    }
}