using MilGlorian.Application.Abstract.Repositories.VacancyDetails;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.VacancyDetails;

public class VacancyDetailWriteRepository : WriteRepository<VacancyDetail>, IVacancyDetailWriteRepository
{
    public VacancyDetailWriteRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
