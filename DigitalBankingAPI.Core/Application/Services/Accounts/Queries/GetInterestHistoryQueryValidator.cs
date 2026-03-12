using FluentValidation;

namespace DigitalBankingAPI.Core.Application.Services.Accounts.Queries
{
    public class GetInterestHistoryQueryValidator : AbstractValidator<GetInterestHistoryQuery>
    {
        public GetInterestHistoryQueryValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty()
                .WithMessage("AccountId is required")
                .Matches("^[a-zA-Z0-9]+$")
                .WithMessage("AccountId must be alphanumeric without special characters")
                .MaximumLength(50)
                .WithMessage("AccountId cannot exceed 50 characters");
        }
    }
}
