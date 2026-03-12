using FluentValidation;

namespace DigitalBankingAPI.Core.Application.Services.Transfers.Commands
{
    public class CreateTransferCommandValidator : AbstractValidator<CreateTransferCommand>
    {
        public CreateTransferCommandValidator()
        {
            RuleFor(x => x.FromAccountId)
                .NotEmpty()
                .WithMessage("FromAccountId is required")
                .Matches("^[a-zA-Z0-9]+$")
                .WithMessage("FromAccountId must be alphanumeric without special characters")
                .MaximumLength(50);

            RuleFor(x => x.ToAccountId)
                .NotEmpty()
                .WithMessage("ToAccountId is required")
                .Matches("^[a-zA-Z0-9]+$")
                .WithMessage("ToAccountId must be alphanumeric without special characters")
                .MaximumLength(50);

            RuleFor(x => x)
                .Must(x => x.FromAccountId != x.ToAccountId)
                .WithMessage("Source and destination accounts must be different");

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
