using FluentValidation;

namespace MilGlorian.Application.DTOs.City;

public record CityDTO
{
    public required string Name { get; set; }
}

public class CityDTOValidator : AbstractValidator<CityDTO>
{
    public CityDTOValidator()
    {
        RuleFor(city => city.Name)
            .NotEmpty().WithMessage("City name is required.")
            .Length(2, 100).WithMessage("City name must be between 2 and 100 characters.");
    }
}