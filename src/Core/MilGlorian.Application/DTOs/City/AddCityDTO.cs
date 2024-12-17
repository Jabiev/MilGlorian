using FluentValidation;

namespace MilGlorian.Application.DTOs.City;

public record AddCityDTO
{
    public required string Name { get; set; }
}
