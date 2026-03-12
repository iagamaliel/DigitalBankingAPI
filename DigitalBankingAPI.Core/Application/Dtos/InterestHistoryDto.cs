
namespace DigitalBankingAPI.Core.Application.Dtos
{
    public class InterestHistoryDto
    {
        public int Id { get; set; }
        public decimal InterestRate { get; set; }
        public decimal CalculatedInterest { get; set; }
        public DateTime CalculationDate { get; set; }
    }
}
