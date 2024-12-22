namespace MilGlorian.Application.DTOs.City;

public record UpdateCityDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
