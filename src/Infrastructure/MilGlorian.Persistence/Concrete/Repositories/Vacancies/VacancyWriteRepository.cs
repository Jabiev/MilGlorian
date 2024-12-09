using MilGlorian.Application.Abstract.Repositories.Vacancies;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Vacancies;

public class VacancyWriteRepository : WriteRepository<Vacancy>, IVacancyWriteRepository
{
    public VacancyWriteRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
