using MilGlorian.Domain.Entities.Common;

namespace MilGlorian.Domain.Entities;

public class Department : BaseEntity
{
    public string Name { get; set; }
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public ICollection<Vacancy> Vacancies { get; set; }
}
