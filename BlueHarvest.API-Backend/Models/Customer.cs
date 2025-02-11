namespace BlueHarvestAPI
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public List<Account> Accounts { get; set; }
    }

}

