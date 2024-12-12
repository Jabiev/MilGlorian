using MilGlorian.Application.Abstract.Repositories.Departments;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Departments;

public class DepartmentReadRepository : ReadRepository<Department>, IDepartmentReadRepository
{
    public DepartmentReadRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}