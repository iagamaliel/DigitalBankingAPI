using DigitalBankingAPI.Core.Application.Dtos;
using MediatR;

namespace DigitalBankingAPI.Core.Application.Services.Transfers.Commands
{
    public class CreateTransferCommand : IRequest<Response<TransferDto>>
    {
        public string FromAccountId { get; set; }
        public string ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }

}
