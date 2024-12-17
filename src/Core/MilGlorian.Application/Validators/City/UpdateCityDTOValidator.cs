using FluentValidation;
using MilGlorian.Application.DTOs.City;

namespace MilGlorian.Application.Validators.City;

public class UpdateCityDTOValidator : AbstractValidator<AddCityDTO>
{
    public UpdateCityDTOValidator()
    {
        RuleFor(city => city.Name)
            .NotEmpty().WithMessage("City name is required.")
            .Length(2, 100).WithMessage("City name must be between 2 and 100 characters.");
    }
}
