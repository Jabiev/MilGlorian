using Microsoft.EntityFrameworkCore;

namespace MilGlorian.Application.Abstract.Repositories;

public interface IRepository<T> where T : class
{
    DbSet<T> Table { get; }
}
