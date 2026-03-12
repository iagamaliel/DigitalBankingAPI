using DigitalBankingAPI.Core.Models;

namespace DigitalBankingAPI.Core.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> CreateDepositAsync(string id, decimal amount, CancellationToken cancellationToken);
        Task<AccountDetails> GetAccountInfoAsync(string accountId, CancellationToken cancellationToken);
        Task<Account> CreateWithdrawalAsync(string accountId, decimal amount, CancellationToken cancellationToken);
        Task<List<InterestHistory>> GetInterestHistoryAsync(string accountId, CancellationToken cancellationToken);
    }
}
