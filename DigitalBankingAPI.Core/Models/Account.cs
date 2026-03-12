namespace DigitalBankingAPI.Core.Models
{
    public class Account
    {
        public string AccountId { get; set; }
        public string CustomerName { get; set; }
        public decimal Balance { get; set; }
    }

    public class AccountDetails
    {
        public string AccountId { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> LastTransactions { get; set; } = new();
        public decimal TotalInterest { get; set; }
    }

}
