using MilGlorian.Application.Abstract.Repositories.Industries;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Industries;

public class IndustryWriteRepository : WriteRepository<Industry>, IIndustryWriteRepository
{
    public IndustryWriteRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
