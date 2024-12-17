namespace MilGlorian.Application.DTOs.City;

public record GetCityDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
