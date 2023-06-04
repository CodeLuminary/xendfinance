namespace Xend_Finance.Models
{
    public class TransactionsModel
    {

        public int TransactionId { get; set; }
        public string TransactionRef { get; set; }
        public string ClientId { get; set; }
        public string WalletAddress { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
