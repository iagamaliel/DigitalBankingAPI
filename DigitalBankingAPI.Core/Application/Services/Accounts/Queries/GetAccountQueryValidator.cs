using FluentValidation;

namespace DigitalBankingAPI.Core.Application.Services.Accounts.Queries
{
    public class GetAccountQueryValidator : AbstractValidator<GetAccountQuery>
    {
        public GetAccountQueryValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty()
                .WithMessage("AccountId is required")
                .MaximumLength(50)
                .WithMessage("AccountId cannot exceed 50 characters")
                .Matches("^[a-zA-Z0-9]+$")
                .WithMessage("AccountId must be alphanumeric without special characters");
        }
    }
}
