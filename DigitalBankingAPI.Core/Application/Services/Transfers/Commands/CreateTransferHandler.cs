using AutoMapper;
using DigitalBankingAPI.Core.Application.Dtos;
using DigitalBankingAPI.Core.Application.Services.Accounts.Commands;
using DigitalBankingAPI.Core.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DigitalBankingAPI.Core.Application.Services.Transfers.Commands
{
    public class CreateTransferCommandHandler
    : IRequestHandler<CreateTransferCommand, Response<TransferDto>>
    {
        private readonly ITransferRepository _transferRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateWithdrawalCommandHandler> _logger;

        public CreateTransferCommandHandler(ITransferRepository transferRepository, IMapper mapper,
            ILogger<CreateWithdrawalCommandHandler> logger)
        {
            _transferRepository = transferRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<TransferDto>> Handle(
            CreateTransferCommand request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Transfer requested. FromAccount: {FromAccountId}, ToAccount: {ToAccountId}, Amount: {Amount}",
                request.FromAccountId,
                request.ToAccountId,
                request.Amount);

            var transfer = await _transferRepository.CreateTransferAsync(
                request.FromAccountId,
                request.ToAccountId,
                request.Amount,
                cancellationToken);

            if (transfer == null)
            {
                _logger.LogWarning(
                    "Transfer failed. FromAccount: {FromAccountId}, ToAccount: {ToAccountId}, Amount: {Amount}",
                    request.FromAccountId,
                    request.ToAccountId,
                    request.Amount);

                return Response<TransferDto>.Fail("Transfer could not be completed", 400);
            }

            var dto = _mapper.Map<TransferDto>(transfer);

            _logger.LogInformation(
                "Transfer completed successfully. FromAccount: {FromAccountId}, ToAccount: {ToAccountId}, Amount: {Amount}",
                request.FromAccountId,
                request.ToAccountId,
                request.Amount);

            return Response<TransferDto>.Success(dto);
        }
    }
}
