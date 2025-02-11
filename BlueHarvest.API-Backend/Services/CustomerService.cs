using Microsoft.EntityFrameworkCore;
using static BlueHarvestAPI.Customer;

namespace BlueHarvestAPI
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public Customer GetCustomerInfo(int customerId)
        {
            try
            {

                Customer customer = _context.Customers
                    .Where(c => c.CustomerId == customerId)
                    .Select(c => new Customer
                    {
                        CustomerId = c.CustomerId,
                        Name = c.Name,
                        Surname = c.Surname,
                        Accounts = c.Accounts.Select(a => new Account
                        {
                            AccountId = a.AccountId,
                            AccType = a.AccType,
                            Balance = a.Balance,
                            Transactions = a.Transactions.Select(t => new Transaction
                            {
                                TransactionId = t.TransactionId,
                                Amount = t.Amount,
                                Date = t.Date
                            }).ToList()
                        }).ToList()
                    })
                    .FirstOrDefault();

                return customer;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public void CreateDummyCustomers()
        {
            CreateCustomerAsync("Aamena", "Bohra", 100);
            CreateCustomerAsync("John", "Doe", 200);
            CreateCustomerAsync("Marcus", "Lame", 300);
            CreateCustomerAsync("Oliver", "Smith", 400);
            CreateCustomerAsync("Peter", "Parker", 500);


        }

        public async Task<Customer> CreateCustomerAsync(string name, string surname, double initialDeposit)
        {
            var existingCustomer = await _context.Customers
                .AnyAsync(c => c.Name == name && c.Surname == surname);

            if (existingCustomer)
                throw new InvalidOperationException("Customer with the same name and surname already exists");

            var customer = new Customer
            {
                Name = name,
                Surname = surname,
                Accounts = new List<Account>()
            };

            try
            {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();

                var accountService = new AccountService(_context);
                var savingsAccount = await accountService.OpenAccountAsync(customer.CustomerId, initialDeposit, AccountType.Savings);

            }
            catch
            {
                throw;
            }

            return customer;
        }

    }
}
