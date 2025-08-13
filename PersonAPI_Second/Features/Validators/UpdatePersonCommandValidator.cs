using FluentValidation;

namespace PersonAPI_Second.Features.Validators
{
    public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonWithIdCommand>
    {
        public UpdatePersonCommandValidator()
        {
            RuleFor(command => command.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(command => command.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

            RuleFor(command => command.DOB)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of birth must be in the past.");

            RuleFor(command => command.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(100).WithMessage("Address must not exceed 100 characters.");
        }
    }
}
