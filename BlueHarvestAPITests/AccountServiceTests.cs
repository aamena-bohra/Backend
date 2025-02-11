using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using BlueHarvestAPI;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlueHarvest.Tests
{
    public class AccountServiceTests
    {
        private readonly AppDbContext _context;
        private readonly AccountService _accountService;

        public AccountServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb") // Use in-memory database for unit testing
                .Options;

            _context = new AppDbContext(options);
            _context.Database.EnsureCreated();

            _accountService = new AccountService(_context);
        }

        [Fact]
        public async Task OpenAccountAsync_CreatesAccount_WhenCustomerExists()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "John", Surname = "Doe" };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var initialDeposit = 200;
            var accountType = AccountType.Current;

            // Act
            var account = await _accountService.OpenAccountAsync(customer.CustomerId, initialDeposit, accountType);

            // Assert
            Assert.NotNull(account);
            Assert.Equal(initialDeposit, account.Balance);
            Assert.Equal(accountType, account.AccType);
            Assert.Contains(account, customer.Accounts);
        }

        [Fact]
        public async Task OpenAccountAsync_ThrowsException_WhenCustomerDoesNotExist()
        {
            // Arrange
            var nonExistingCustomerId = 999;
            var initialDeposit = 200;
            var accountType = AccountType.Current;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _accountService.OpenAccountAsync(nonExistingCustomerId, initialDeposit, accountType)
            );
            Assert.Equal("Customer does not exist", exception.Message);
        }

    }
}
