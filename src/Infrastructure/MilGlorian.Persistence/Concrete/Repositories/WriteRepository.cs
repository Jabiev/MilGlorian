using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MilGlorian.Application.Abstract.Repositories;
using MilGlorian.Domain.Entities.Common;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly MilGlorianDbContext _milGlorianDbContext;
    public WriteRepository(MilGlorianDbContext milGlorianDbContext)
    {
        _milGlorianDbContext = milGlorianDbContext;
    }

    public DbSet<T> Table => _milGlorianDbContext.Set<T>();

    public async Task<int> SaveChangesAsync() => await _milGlorianDbContext.SaveChangesAsync();

    public async Task AddRangeAsync(List<T> entities) => await Table.AddRangeAsync(entities);

    public async Task<T> AddAsync(T entity)
    {
        await Table.AddAsync(entity);
        return entity;
    }

    public void Remove(T entity) => _milGlorianDbContext.Remove(entity);

    public bool Update(T entity)
    {
        EntityEntry entry = Table.Update(entity);
        return entry.State == EntityState.Modified;
    }
}
