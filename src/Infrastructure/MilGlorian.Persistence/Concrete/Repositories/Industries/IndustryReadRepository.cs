using MilGlorian.Application.Abstract.Repositories.Industries;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Industries;

public class IndustryReadRepository : ReadRepository<Industry>, IIndustryReadRepository
{
    public IndustryReadRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}