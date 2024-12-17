using MilGlorian.Application.Abstract.Repositories.Departments;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Departments;

public class DepartmentWriteRepository : WriteRepository<Department>, IDepartmentWriteRepository
{
    public DepartmentWriteRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
