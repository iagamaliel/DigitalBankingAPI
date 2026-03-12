using DigitalBankingAPI.Core.Application.Services.Interest.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankingAPI.Controllers.Controllers.v1
{
    [ApiController]
    [Route("api/v1/interest")]
    public class InterestController : BaseApiController
    {
        public InterestController(IMediator mediator) : base()
        {

        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateInterest()
        {
            var command = new CalculateDailyInterestCommand();

            var result = await Mediator.Send(command);

            return HandleResponse(result);
        }
    }
}
