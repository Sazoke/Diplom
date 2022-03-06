using Infrastructure.Models.Application;
using Infrastructure.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T> where T : BaseAuditableEntity
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T> GetById(long id, Func<IQueryable<T>, IQueryable<T>>? includes = null) => includes(await Get()).FirstOrDefault(e => e.Id == id);

    public async Task<IEnumerable<T>> GetByIds(Func<IQueryable<T>, IQueryable<T>>? includes, IEnumerable<long> ids) =>
         includes(await Get()).Where(e => ids.Contains(e.Id));

    public async Task<IEnumerable<T>> GetMany(Func<IQueryable<T>, IQueryable<T>>? includes = null)
    {
        return includes is null ? await Get() : includes(await Get());
    }

    private async Task<IQueryable<T>> Get() => _context.Set<T>().AsQueryable();


    public async Task Insert(T entity)
    {
        entity.Created = DateTime.Now;
        _context.Add(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Update(T entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public virtual async Task Delete(long id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task Delete(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
}