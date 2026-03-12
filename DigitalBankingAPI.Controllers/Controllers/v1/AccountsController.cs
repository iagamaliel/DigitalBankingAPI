using DigitalBankingAPI.Core.Application.Dtos;
using DigitalBankingAPI.Core.Application.Services.Accounts.Commands;
using DigitalBankingAPI.Core.Application.Services.Accounts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankingAPI.Controllers.Controllers.v1
{
    [ApiController]
    [Route("api/v1/accounts")]
    public class AccountsController : BaseApiController
    {
        public AccountsController(IMediator mediator) : base()
        {
        }

        [HttpPost("{accountId}/deposit")]
        public async Task<IActionResult> Deposit(
            string accountId,
            [FromBody] DepositRequest request)
        {
            var command = new CreateDepositCommand
            {
                AccountId = accountId,
                Amount = request.Amount
            };

            var result = await Mediator.Send(command);

            return HandleResponse(result);
        }

        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetAccount(string accountId)
        {
            var result = await Mediator.Send(new GetAccountQuery(accountId));

            return HandleResponse(result);
        }

        [HttpGet("{accountId}/interest-history")]
        public async Task<IActionResult> GetInterestHistory(string accountId)
        {
            var query = new GetInterestHistoryQuery(accountId);

            var result = await Mediator.Send(query);

            return HandleResponse(result);
        }
    }
}
