

namespace DigitalBankingAPI.Core.Application.Dtos
{
    public class TransactionDto
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
