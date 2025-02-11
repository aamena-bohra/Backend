namespace BlueHarvestAPI
{
	public interface IAccountService
	{
        Task<Account> OpenAccountAsync(int customerId, double initialCredit, AccountType type);
    }

}
