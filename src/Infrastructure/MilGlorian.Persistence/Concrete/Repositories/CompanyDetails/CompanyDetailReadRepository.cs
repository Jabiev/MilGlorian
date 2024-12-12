using MilGlorian.Application.Abstract.Repositories.CompanyDetails;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.CompanyDetails;

public class CompanyDetailReadRepository : ReadRepository<CompanyDetail>, ICompanyDetailReadRepository
{
    public CompanyDetailReadRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}