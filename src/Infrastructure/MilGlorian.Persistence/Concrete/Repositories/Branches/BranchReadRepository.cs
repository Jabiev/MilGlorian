using MilGlorian.Application.Abstract.Repositories.Branches;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Branches;

public class BranchReadRepository : ReadRepository<Branch>, IBranchReadRepository
{
    public BranchReadRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
