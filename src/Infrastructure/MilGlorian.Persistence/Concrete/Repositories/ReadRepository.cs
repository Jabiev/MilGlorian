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

    public IQueryable<T> GetAll() => Table.AsNoTracking();
    public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null,
        Expression<Func<T, object?>> orderBy = null,
        bool ascending = true,
        bool isTracking = true,
        int skip = 0,
        int take = 10,
        params string[] includes
        )
    {
        IQueryable<T> query = Table;

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (expression is not null)
            query = query.Where(expression);

        if (orderBy is not null)
            query = ascending
                ? query.OrderBy(orderBy)
                : query.OrderByDescending(orderBy);

        query = query
            .Skip(skip)
            .Take(take);

        if (!isTracking)
            query = query.AsNoTracking();

        return query;
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        => Table.Where(expression);

    public IQueryable<T> WhereAsNoTracking(Expression<Func<T, bool>> expression)
        => Table.AsNoTracking().Where(expression);

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool tracking = true)
        => await (tracking ? Table : Table.AsNoTracking())
        .FirstOrDefaultAsync(expression);

    public async Task<T?> GetByIdAsync(Guid id, bool tracking = true)
        => await (tracking ? Table : Table.AsNoTracking())
        .FirstOrDefaultAsync(data => data.Id == id);

    public async Task<T?> GetByFiltered(Expression<Func<T, bool>> expression)
        => await Table.Where(expression).AsNoTracking().FirstOrDefaultAsync();
}
