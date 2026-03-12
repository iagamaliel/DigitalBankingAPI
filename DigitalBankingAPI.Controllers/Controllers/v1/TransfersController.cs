using DigitalBankingAPI.Core.Application.Dtos;
using DigitalBankingAPI.Core.Application.Services.Transfers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankingAPI.Controllers.Controllers.v1
{
    [ApiController]
    [Route("api/v1/accounts")]
    public class TransfersController : BaseApiController
    {
        public TransfersController(IMediator mediator) : base()
        {

        }

        [HttpPost]
        public async Task<IActionResult> CreateTransfer([FromBody] TransferRequest request)
        {
            var command = new CreateTransferCommand
            {
                FromAccountId = request.FromAccountId,
                ToAccountId = request.ToAccountId,
                Amount = request.Amount
            };

            var result = await Mediator.Send(command);

            return HandleResponse(result);
        }
    }
}
