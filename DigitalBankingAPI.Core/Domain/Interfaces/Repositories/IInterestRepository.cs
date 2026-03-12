
namespace DigitalBankingAPI.Core.Domain.Interfaces.Repositories
{
    public interface IInterestRepository
    {
        Task<(bool Success, string Message)> CalculateDailyInterestAsync(
            CancellationToken cancellationToken);
    }
}
