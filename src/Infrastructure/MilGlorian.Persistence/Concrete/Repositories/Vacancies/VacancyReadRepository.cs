using MilGlorian.Application.Abstract.Repositories.Vacancies;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Vacancies;

public class VacancyReadRepository : ReadRepository<Vacancy>, IVacancyReadRepository
{
    public VacancyReadRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
