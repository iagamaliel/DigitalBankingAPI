using AutoMapper;
using DigitalBankingAPI.Core.Application.Dtos;
using DigitalBankingAPI.Core.Domain.Interfaces.Repositories;
using MediatR;

namespace DigitalBankingAPI.Core.Application.Services.Accounts.Queries
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, Response<AccountDetailsDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public GetAccountQueryHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Response<AccountDetailsDto>> Handle(
            GetAccountQuery request,
            CancellationToken cancellationToken)
        {
            var account = await _accountRepository
                .GetAccountInfoAsync(request.AccountId, cancellationToken);

            if (account == null)
                return Response<AccountDetailsDto>.Fail("Account not found", 404);

            var dto = _mapper.Map<AccountDetailsDto>(account);

            return Response<AccountDetailsDto>.Success(dto);
        }
    }

}
