using Microsoft.EntityFrameworkCore;
using static BlueHarvestAPI.Account;

namespace BlueHarvestAPI
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Account> OpenAccountAsync(int customerId, double initialDeposit, AccountType type)
        {
            try
            {
                var customer = await _context.Customers
                    .Include(c => c.Accounts) // Ensure accounts are loaded
                    .FirstOrDefaultAsync(c => c.CustomerId == customerId);

                if (customer == null)
                    throw new InvalidOperationException("Customer does not exist");

                if (type == AccountType.Savings && initialDeposit < 100)
                    throw new InvalidOperationException("Initial deposit must be at least 100 for a savings account");

                var account = new Account
                {
                    Balance = initialDeposit,
                    AccType = type,
                    Transactions = new List<Transaction>()
                };

                if (initialDeposit > 0)
                {
                    var transaction = new Transaction
                    {
                        Amount = initialDeposit,
                        Date = DateTime.UtcNow,
                        Type = TransactionType.Deposit
                    };

                    account.Transactions.Add(transaction);
                }


                // Add the new account
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();

                // Assign account to customer and update
                customer.Accounts.Add(account);
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();

                return account;
            }
            catch (Exception ex)
            {
                // Log exception
                throw ex;
            }
        }


    }

}

