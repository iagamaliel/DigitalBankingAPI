using DigitalBankingAPI.Core.Application.Dtos;
using MediatR;

namespace DigitalBankingAPI.Core.Application.Services.Interest.Commands
{
    public class CalculateDailyInterestCommand : IRequest<Response<string>>
    {
    }
}
