using FluentValidation;

namespace DigitalBankingAPI.Core.Application.Services.Accounts.Commands
{
    public class CreateDepositCommandValidator : AbstractValidator<CreateDepositCommand>
    {
        public CreateDepositCommandValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty()
                .MaximumLength(50)
                .Matches("^[a-zA-Z0-9]+$")
                .WithMessage("AccountId must be alphanumeric without special characters");

            RuleFor(x => x.Amount)
            .NotNull()
            .WithMessage("Amount is required")
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero")
            .PrecisionScale(18, 2, true)
            .WithMessage("Amount cannot have more than 2 decimal places");
        }
    }
}
