using MilGlorian.Domain.Entities.Common;

namespace MilGlorian.Domain.Entities;

public class Company : BaseEntity
{
    public string Name { get; set; }
    public int EmployeeCount { get; set; }
    public DateTime FoundedDate { get; set; }
    public string LogoPath { get; set; }
    public string PosterPath { get; set; }
    public int ExistedVacancy { get; set; }
    public Guid IndustryId { get; set; }
    public Industry Industry { get; set; }
    public CompanyDetail CompanyDetail { get; set; }
    public CompanySize CompanySize { get; set; }
    public ICollection<Biography> Biographies { get; set; }
    public ICollection<Branch> Branches { get; set; }
    public ICollection<Department>? Departments { get; set; }
    public ICollection<Vacancy> Vacancies { get; set; }
}
