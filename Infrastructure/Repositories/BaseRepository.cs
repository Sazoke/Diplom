using Infrastructure.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> where T : BaseAuditableEntity
{
    protected readonly DbContext Context;

    public BaseRepository(DbContext context)
    {
        Context = context;
    }
    
    public async Task<T> GetById(long id, Func<IQueryable<T>, IQueryable<T>>? queries = null) => queries(await GetAll())
        .FirstOrDefault(e => e.Id == id);

    public async Task<IEnumerable<T>> GetByIds (IEnumerable<long> ids, Func<IQueryable<T>, IQueryable<T>>? queries = null) =>
        queries(await GetAll()).Where(e => ids.Contains(e.Id));

    public async Task<IEnumerable<T>> GetAll(Func<IQueryable<T>, IQueryable<T>>? queries = null)
    {
        return queries is null ? await GetAll() : queries(await GetAll());
    }

    private async Task<IQueryable<T>> GetAll() => Context.Set<T>().AsQueryable();


    public async Task<T> AddAsync(T entity)
    {
        entity.Created = DateTime.Now;
        Context.Add(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        Context.Update(entity);
        await Context.SaveChangesAsync();
    }
    
    public virtual async Task Delete(long id)
    {
        var entity = await Context.Set<T>().FindAsync(id);
        Context.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task Delete(T entity)
    {
        Context.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task SaveChangesAsync() => await Context.SaveChangesAsync();
}
    }

    public virtual async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}