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

    public async Task<bool> AddAsync(T entity)
    {
        EntityEntry entry = await Table.AddAsync(entity);
        return entry.State == EntityState.Added;
    }

    public async Task<bool> Remove(Guid id)
    {
        T entity = await Table.FirstOrDefaultAsync(data => data.Id == id);
        var entry = _milGlorianDbContext.Remove(entity);
        return entry.State == EntityState.Deleted;
    }

    public bool Update(T entity)
    {
        EntityEntry entry = Table.Update(entity);
        return entry.State == EntityState.Modified;
    }
}
