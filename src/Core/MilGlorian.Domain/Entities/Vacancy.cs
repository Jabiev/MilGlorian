using MilGlorian.Domain.Entities.Common;

namespace MilGlorian.Domain.Entities;

public class Vacancy : BaseEntity
{
    public string Title { get; set; }
    public VacancyType VacancyType { get; set; }
    public ICollection<Category> Category { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public Guid BranchId { get; set; }
    public Branch Branch { get; set; }
    public VacancyDetail VacancyDetail { get; set; }
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }
    public Guid CityId { get; set; }
    public City City { get; set; }
    public bool IsSalaryVisible { get; set; }
    public bool IsRemote { get; set; }
    public JobLevel JobLevel { get; set; }
    public int ViewCount { get; set; }
    public string? Location { get; set; }
    public DateTime ExpireDate { get; set; }
}
