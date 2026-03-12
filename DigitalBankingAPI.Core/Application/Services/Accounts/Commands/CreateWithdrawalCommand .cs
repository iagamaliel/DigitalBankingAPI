using DigitalBankingAPI.Core.Application.Dtos;
using MediatR;

namespace DigitalBankingAPI.Core.Application.Services.Accounts.Commands
{
    public class CreateWithdrawalCommand : IRequest<Response<AccountDto>>
    {
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
