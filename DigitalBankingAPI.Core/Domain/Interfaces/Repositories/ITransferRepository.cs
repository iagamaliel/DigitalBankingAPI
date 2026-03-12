using DigitalBankingAPI.Core.Models;

namespace DigitalBankingAPI.Core.Domain.Interfaces.Repositories
{
    public interface ITransferRepository
    {
        Task<Transfer> CreateTransferAsync(
            string fromAccountId,
            string toAccountId,
            decimal amount,
            CancellationToken cancellationToken);
    }
}
