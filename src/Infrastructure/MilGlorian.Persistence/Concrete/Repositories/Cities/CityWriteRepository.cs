using MilGlorian.Application.Abstract.Repositories.Cities;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Cities;

public class CityWriteRepository : WriteRepository<City>, ICityWriteRepository
{
    public CityWriteRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
