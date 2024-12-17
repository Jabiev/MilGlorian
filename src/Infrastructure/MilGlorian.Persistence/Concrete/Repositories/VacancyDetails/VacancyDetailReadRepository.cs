using MilGlorian.Application.Abstract.Repositories.VacancyDetails;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.VacancyDetails;

public class VacancyDetailReadRepository : ReadRepository<VacancyDetail>, IVacancyDetailReadRepository
{
    public VacancyDetailReadRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}