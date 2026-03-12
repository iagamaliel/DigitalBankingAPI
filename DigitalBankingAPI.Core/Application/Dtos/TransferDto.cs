

namespace DigitalBankingAPI.Core.Application.Dtos
{
    public class TransferDto
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; }
    }
}
