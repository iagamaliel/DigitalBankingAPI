using AutoMapper;
using DigitalBankingAPI.Core.Application.Dtos;
using DigitalBankingAPI.Core.Domain.Interfaces.Repositories;
using MediatR;

namespace DigitalBankingAPI.Core.Application.Services.Accounts.Commands
{
    public class CreateDepositCommandHandler : IRequestHandler<CreateDepositCommand, Response<AccountDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public CreateDepositCommandHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Response<AccountDto>> Handle(
            CreateDepositCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Amount <= 0)
                return Response<AccountDto>
                    .Fail("Amount must be greater than zero", 400);

            var account = await _accountRepository.CreateDepositAsync(
                request.AccountId,
                request.Amount,
                cancellationToken);

            if (account == null)
                return Response<AccountDto>
                    .Fail("Account not found", 404);

            var dto = _mapper.Map<AccountDto>(account);

            return Response<AccountDto>.Success(dto);
        }
    }
}
