namespace MilGlorian.Application.DTOs.Industry;

public record GetIndustryDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
