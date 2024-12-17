using MilGlorian.Application.Abstract.Repositories.Companies;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Companies;

public class CompanyWriteRepository : WriteRepository<Category>, ICompanyWriteRepository
{
    public CompanyWriteRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
