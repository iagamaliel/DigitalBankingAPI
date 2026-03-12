using AutoMapper;
using DigitalBankingAPI.Core.Application.Dtos;
using DigitalBankingAPI.Core.Domain.Interfaces.Repositories;
using MediatR;

namespace DigitalBankingAPI.Core.Application.Services.Accounts.Queries
{
    public class GetInterestHistoryQueryHandler
    : IRequestHandler<GetInterestHistoryQuery, Response<List<InterestHistoryDto>>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetInterestHistoryQueryHandler(
            IAccountRepository accountRepository,
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<InterestHistoryDto>>> Handle(
          GetInterestHistoryQuery request,
          CancellationToken cancellationToken)
        {
            var history = await _accountRepository.GetInterestHistoryAsync(
                request.AccountId,
                cancellationToken);

            if (history == null || !history.Any())
                return Response<List<InterestHistoryDto>>
                    .Fail("No interest history found", 404);

            var dto = _mapper.Map<List<InterestHistoryDto>>(history);

            return Response<List<InterestHistoryDto>>.Success(dto);
        }
    }
}
