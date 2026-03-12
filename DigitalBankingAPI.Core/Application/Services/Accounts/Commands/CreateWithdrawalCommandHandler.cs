using AutoMapper;
using DigitalBankingAPI.Core.Application.Dtos;
using DigitalBankingAPI.Core.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DigitalBankingAPI.Core.Application.Services.Accounts.Commands
{
    public class CreateWithdrawalCommandHandler : IRequestHandler<CreateWithdrawalCommand, Response<AccountDto>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateWithdrawalCommandHandler> _logger;

        public CreateWithdrawalCommandHandler(IAccountRepository accountRepository, IMapper mapper, 
            ILogger<CreateWithdrawalCommandHandler> logger)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<AccountDto>> Handle(
            CreateWithdrawalCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Withdrawal requested. AccountId: {AccountId}, Amount: {Amount}",
                request.AccountId,
                request.Amount);

            if (request.Amount <= 0)
            {
                _logger.LogWarning(
                    "Invalid withdrawal amount for AccountId {AccountId}",
                    request.AccountId);

                return Response<AccountDto>
                    .Fail("Amount must be greater than zero", 400);
            }

            var account = await _accountRepository.CreateWithdrawalAsync(
                request.AccountId,
                request.Amount,
                cancellationToken);

            if (account == null)
            {
                _logger.LogWarning(
                    "Withdrawal failed. Account not found: {AccountId}",
                    request.AccountId);

                return Response<AccountDto>.Fail("Account not found", 404);
            }

            var dto = _mapper.Map<AccountDto>(account);

            _logger.LogInformation(
                "Withdrawal successful. AccountId: {AccountId}, NewBalance: {Balance}",
                account.AccountId,
                account.Balance);

            return Response<AccountDto>.Success(dto);
        }
    }
}
