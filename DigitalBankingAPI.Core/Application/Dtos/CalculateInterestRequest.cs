
namespace DigitalBankingAPI.Core.Application.Dtos
{
    public class CalculateInterestRequest
    {
        public string AccountId { get; set; }
        public decimal InterestRate { get; set; }
    }
}
