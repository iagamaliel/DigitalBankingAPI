using DigitalBankingAPI.Core.Application.Dtos;
using MediatR;

namespace DigitalBankingAPI.Core.Application.Services.Accounts.Queries
{
    public class GetAccountQuery : IRequest<Response<AccountDetailsDto>>
    {
        public string AccountId { get; set; }

        public GetAccountQuery(string accountId)
        {
            AccountId = accountId;
        }
    }
}
