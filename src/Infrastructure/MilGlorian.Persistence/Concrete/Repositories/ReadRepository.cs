using Microsoft.EntityFrameworkCore;
using MilGlorian.Application.Abstract.Repositories;
using MilGlorian.Domain.Entities.Common;
using MilGlorian.Persistence.Contexts;
using System.Linq.Expressions;

namespace MilGlorian.Persistence.Concrete.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly MilGlorianDbContext _milGlorianDbContext;
    public ReadRepository(MilGlorianDbContext milGlorianDbContext)
    {
        _milGlorianDbContext = milGlorianDbContext;
    }

    public DbSet<T> Table => _milGlorianDbContext.Set<T>();
    public IQueryable<T> GetAllAsNoTracking() => Table;

    public IQueryable<T> Where(Expression<Func<T, bool>> func) => Table.Where(func);

    public IQueryable<T> WhereAsNoTracking(Expression<Func<T, bool>> func) => Table.AsNoTracking().Where(func);

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> func, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking().Where(func);
        return await query.FirstOrDefaultAsync(func);
    }

    public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(data => data.Id == id);
    }
}
