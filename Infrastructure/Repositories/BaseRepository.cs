using Infrastructure.Models.Application;
using Infrastructure.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> where T : BaseAuditableEntity
{
    private readonly DbContext _context;
    private readonly ApplicationContext _applicationContext;

    public BaseRepository(DbContext context, ApplicationContext applicationContext)
    {
        _context = context;
        _applicationContext = applicationContext;
    }
    
    public async Task<T> GetById(long id, Func<IQueryable<T>, IQueryable<T>>? queries = null)
    {
        return queries is null
            ? (await GetAll()).FirstOrDefault(e => e.Id == id)
            : queries(await GetAll()).FirstOrDefault(e => e.Id == id);
    }

    public async Task<IEnumerable<T>> GetByIds (IEnumerable<long> ids, Func<IQueryable<T>, IQueryable<T>>? queries = null)
    { 
        return queries is null
            ? (await GetAll()).Where(e => ids.Contains(e.Id))
            : queries(await GetAll()).Where(e => ids.Contains(e.Id));
    }

    public async Task<IEnumerable<T>> GetAll(Func<IQueryable<T>, IQueryable<T>>? queries = null)
    {
        return queries is null ? await GetAll() : queries(await GetAll());
    }

    private async Task<IQueryable<T>> GetAll() => _context.Set<T>().AsQueryable();

    public async Task<T> AddAsync(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.CreatedById = _applicationContext.CurrentUserId;
        _context.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public virtual async Task Delete(long id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if(entity is null)
            return;
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Delete(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}