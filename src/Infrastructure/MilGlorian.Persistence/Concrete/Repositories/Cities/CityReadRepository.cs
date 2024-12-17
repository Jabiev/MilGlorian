using MilGlorian.Application.Abstract.Repositories.Cities;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Cities;

public class CityReadRepository : ReadRepository<City>, ICityReadRepository
{
    public CityReadRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
