using MilGlorian.Application.Abstract.Repositories.Categories;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Contexts;

namespace MilGlorian.Persistence.Concrete.Repositories.Categories;

public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
{
    public CategoryWriteRepository(MilGlorianDbContext milGlorianDbContext) : base(milGlorianDbContext)
    {
    }
}
