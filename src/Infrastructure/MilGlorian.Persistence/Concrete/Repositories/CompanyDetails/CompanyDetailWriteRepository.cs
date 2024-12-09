using MilGlorian.Application.Abstract.Repositories.CompanyDetails;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.CompanyDetails;

public class CompanyDetailWriteRepository : WriteRepository<CompanyDetail>, ICompanyDetailWriteRepository
{
    public CompanyDetailWriteRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
