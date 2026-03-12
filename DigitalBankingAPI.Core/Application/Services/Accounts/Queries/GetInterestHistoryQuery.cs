using DigitalBankingAPI.Core.Application.Dtos;
using MediatR;

namespace DigitalBankingAPI.Core.Application.Services.Accounts.Queries
{
    public class GetInterestHistoryQuery : IRequest<Response<List<InterestHistoryDto>>>
    {
        public string AccountId { get; set; }

        public GetInterestHistoryQuery(string accountId)
        {
            AccountId = accountId;
        }
    }
}
