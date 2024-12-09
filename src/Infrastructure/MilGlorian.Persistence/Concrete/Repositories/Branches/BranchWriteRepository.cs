using MilGlorian.Application.Abstract.Repositories.Branches;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Branches;

public class BranchWriteRepository : WriteRepository<Branch>, IBranchWriteRepository
{
    public BranchWriteRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
