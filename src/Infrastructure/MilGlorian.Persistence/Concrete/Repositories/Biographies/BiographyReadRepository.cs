using MilGlorian.Application.Abstract.Repositories.Biographies;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Biographies;

public class BiographyReadRepository : ReadRepository<Biography>, IBiographyReadRepository
{
    public BiographyReadRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
