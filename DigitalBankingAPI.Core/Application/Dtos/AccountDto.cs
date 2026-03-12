namespace DigitalBankingAPI.Core.Application.Dtos
{
    public class AccountDto
    {
        public string AccountId { get; set; }
        public string CustomerName { get; set; }
        public decimal Balance { get; set; }
    }

    public class AccountDetailsDto
    {
        public string AccountId { get; set; }
        public decimal Balance { get; set; }
        public List<TransactionDto> LastTransactions { get; set; } = new();
        public decimal TotalInterest { get; set; }
    }

}
