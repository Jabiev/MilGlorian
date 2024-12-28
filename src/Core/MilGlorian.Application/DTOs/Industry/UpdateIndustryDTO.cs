namespace MilGlorian.Application.DTOs.Industry;

public record UpdateIndustryDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
