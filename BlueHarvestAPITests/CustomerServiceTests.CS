using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using BlueHarvestAPI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueHarvest.Tests
{
    public class CustomerServiceTests
    {
        private readonly AppDbContext _context;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            // Set up an in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb") // Use an in-memory database for unit testing
                .Options;

            _context = new AppDbContext(options); // Create instance of AppDbContext using in-memory database
            _context.Database.EnsureCreated(); // Ensure that the database is created

            _customerService = new CustomerService(_context);
        }

        [Fact]
        public void GetCustomerInfo_ShouldReturnCustomerInfo_WhenCustomerExists()
        {
            // Arrange
            var customerId = 5;
            var customer = new Customer { CustomerId = customerId, Name = "Aamena", Surname = "Bohra" };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // Act
            var result = _customerService.GetCustomerInfo(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.CustomerId);
            Assert.Equal("Aamena", result.Name);
            Assert.Equal("Bohra", result.Surname);
        }

        [Fact]
        public void GetCustomerInfo_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = 999; // Assume this customer doesn't exist

            // Act
            var result = _customerService.GetCustomerInfo(customerId);

            // Assert
            Assert.Null(result);
        }
    }
}
