using MilGlorian.Application.Abstract.Repositories.Companies;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Companies;

public class CompanyReadRepository : ReadRepository<Category>, ICompanyReadRepository
{
    public CompanyReadRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}