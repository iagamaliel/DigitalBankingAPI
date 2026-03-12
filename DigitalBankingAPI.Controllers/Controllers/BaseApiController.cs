using DigitalBankingAPI.Core.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DigitalBankingAPI.Controllers.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();


        protected IActionResult HandleResponse<T>(Response<T> response)
        {
            if (response == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return StatusCode(response.StatusCode, response);
        }
    }
}
