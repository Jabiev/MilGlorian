namespace MilGlorian.Application.Abstract.Repositories;

public interface IWriteRepository<T> : IRepository<T> where T : class
{
    Task<bool> AddAsync(T entity);
    Task AddRangeAsync(List<T> entities);
    bool Update(T entity);
    Task<bool> Remove(Guid id);
    Task<int> SaveChangesAsync();
}
