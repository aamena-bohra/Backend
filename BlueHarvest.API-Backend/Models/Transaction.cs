namespace BlueHarvestAPI
{
    public enum TransactionType { Deposit, Withdrawal }

    public class Transaction
    {
        public int TransactionId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public TransactionType Type { get; set; }
    }

}

