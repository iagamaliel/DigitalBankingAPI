using DigitalBankingAPI.Core.Application.Dtos;
using DigitalBankingAPI.Core.Domain.Interfaces.Repositories;
using MediatR;

namespace DigitalBankingAPI.Core.Application.Services.Interest.Commands
{
    public class CalculateDailyInterestCommandHandler
    : IRequestHandler<CalculateDailyInterestCommand, Response<string>>
    {
        private readonly IInterestRepository _interestRepository;

        public CalculateDailyInterestCommandHandler(IInterestRepository interestRepository)
        {
            _interestRepository = interestRepository;
        }

        public async Task<Response<string>> Handle(
      CalculateDailyInterestCommand request,
      CancellationToken cancellationToken)
        {
            var result = await _interestRepository.CalculateDailyInterestAsync(cancellationToken);

            if (!result.Success)
                return Response<string>.Fail(result.Message, 400);

            return Response<string>.Success("Daily interest calculated successfully");
        }
    }
}
