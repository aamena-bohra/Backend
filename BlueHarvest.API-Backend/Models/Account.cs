namespace BlueHarvestAPI
{
    public enum AccountType { Savings, Current }

    public class Account
    {
        public int AccountId { get; set; }
        public double Balance { get; set; } = 0;
        public AccountType AccType { get; set; } 
        public List<Transaction> Transactions { get; set; }
    }
}

