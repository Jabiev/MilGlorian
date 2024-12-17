using MilGlorian.Domain.Entities.Common;

namespace MilGlorian.Domain.Entities;

public class Branch : BaseEntity
{
    public string Name { get; set; }
    public Guid CityId { get; set; }
    public Guid CompanyId { get; set; }
    public City City { get; set; }
    public Company Company { get; set; }
    public ICollection<Department>? Departments { get; set; }
    public ICollection<Vacancy> Vacancies { get; set; }
    public bool IsMain { get; set; }
}