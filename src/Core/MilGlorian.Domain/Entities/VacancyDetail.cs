using MilGlorian.Domain.Entities.Common;

namespace MilGlorian.Domain.Entities;

public class VacancyDetail : BaseEntity
{
    public VacancyType VacancyType { get; set; }
    public string Content { get; set; }
    public DateTime ExpireDate { get; set; }
    public Guid VacancyId { get; set; }
    public Vacancy Vacancy { get; set; }
    public string? Salary { get; set; }
    public EducationLevel EducationLevel { get; set; } = EducationLevel.None;
    public string? ExperienceRange { get; set; } //5-8 Years
}