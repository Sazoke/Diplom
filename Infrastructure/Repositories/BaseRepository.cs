using System.Linq.Expressions;
using Infrastructure.Models.Application;
using Infrastructure.Models.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Infrastructure.Repositories;

public class BaseRepository<T> where T : BaseAuditableEntity
{
    protected readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<T> GetById(long id, Func<IQueryable<T>, IQueryable<T>>? queries = null) => queries(await GetAll())
        .FirstOrDefault(e => e.Id == id);

    public async Task<IEnumerable<T>> GetByIds (IEnumerable<long> ids, Func<IQueryable<T>, IQueryable<T>>? queries = null) =>
        queries(await GetAll()).Where(e => ids.Contains(e.Id));

    public async Task<IEnumerable<T>> GetAll(Func<IQueryable<T>, IQueryable<T>>? queries = null)
    {
        return queries is null ? await GetAll() : queries(await GetAll());
    }

    private async Task<IQueryable<T>> GetAll() => _context.Set<T>().AsQueryable();


    public async Task<T> AddAsync(T entity)
    {
        entity.Created = DateTime.Now;
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