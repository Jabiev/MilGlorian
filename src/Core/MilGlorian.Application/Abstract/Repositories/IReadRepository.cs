using System.Linq.Expressions;

namespace MilGlorian.Application.Abstract.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : class
{
    IQueryable<T> GetAllAsNoTracking();
    IQueryable<T> Where(Expression<Func<T, bool>> func);
    IQueryable<T> WhereAsNoTracking(Expression<Func<T, bool>> func);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> func, bool tracking = true);
    Task<T> GetByIdAsync(Guid id, bool tracking = true);
}
