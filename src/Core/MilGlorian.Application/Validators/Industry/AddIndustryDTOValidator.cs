using FluentValidation;
using MilGlorian.Application.DTOs.Industry;

namespace MilGlorian.Application.Validators.Industry;

public class AddIndustryDTOValidator : AbstractValidator<AddIndustryDTO>
{
    public AddIndustryDTOValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty().WithMessage("Industry name is required.")
            .Length(2, 100).WithMessage("Industry name must be between 2 and 100 characters.");
    }
}
