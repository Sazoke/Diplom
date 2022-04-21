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

    public virtual IEnumerable<T> GetByFilter(Filter filter)
    {
        var components = Repository.GetAll(q =>
        {
            if (filter.Text is not null)
                q = q.Where(c => c.Name.Contains(filter.Text));
            if (filter.Tags is not null)
                q = q.Include(c => c.Tags)
                    .Where(c => c.Tags.All(t => filter.Tags.Contains(t.Id)));
            if (filter.SchoolArea is not null)
                q = q.Where(c => filter.SchoolArea == c.AreaId);
            if (filter.DateTime is not null)
                q = q.Where(c => c.CreatedAt >= filter.DateTime);
            if (filter.TeacherId is not null)
                q = q.Where(c => c.CreatedById == filter.TeacherId);
            return q.Skip(filter.Skip)
                .Take(filter.PageSize);
        });
        return components;
    }
}