using MilGlorian.Application.Abstract.Repositories.Biographies;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Biographies;

public class BiographyWriteRepository : WriteRepository<Biography>, IBiographyWriteRepository
{
    public BiographyWriteRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
