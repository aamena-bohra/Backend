using Moq;
using Microsoft.AspNetCore.Mvc;
using BlueHarvest.API.Controllers;
using BlueHarvestAPI;

namespace BlueHarvest.Tests
{
    public class AccountControllerTests
    {
        private readonly Mock<IAccountService> _accountServiceMock;
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _accountServiceMock = new Mock<IAccountService>();
            _controller = new AccountController(_accountServiceMock.Object);
        }

        [Fact]
        public async Task OpenCurrentAccount_ReturnsOkResult_WithAccount()
        {
            // Arrange
            var request = new AccountRequest
            {
                CustomerId = 1,
                InitialDeposit = 200
            };

            var account = new Account
            {
                AccountId = 1,
                Balance = 200,
                AccType = AccountType.Current
            };

            _accountServiceMock.Setup(service => service.OpenAccountAsync(request.CustomerId, request.InitialDeposit, AccountType.Current))
                               .ReturnsAsync(account);

            // Act
            var result = await _controller.OpenCurrentAccount(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Account>(okResult.Value);
            Assert.Equal(account.AccountId, returnValue.AccountId);
            Assert.Equal(account.Balance, returnValue.Balance);
        }

        [Fact]
        public async Task OpenCurrentAccount_ReturnsBadRequest_WhenExceptionOccurs()
        {
            // Arrange
            var request = new AccountRequest
            {
                CustomerId = 1,
                InitialDeposit = 200
            };

            _accountServiceMock.Setup(service => service.OpenAccountAsync(request.CustomerId, request.InitialDeposit, AccountType.Current))
                               .ThrowsAsync(new InvalidOperationException("Customer does not exist"));

            // Act
            var result = await _controller.OpenCurrentAccount(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Customer does not exist", badRequestResult.Value);
        }
    }
}
