
namespace DigitalBankingAPI.Core.Models
{
    public class InterestHistory
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public decimal InterestRate { get; set; }
        public decimal CalculatedInterest { get; set; }
        public DateTime CalculationDate { get; set; }
    }
}
