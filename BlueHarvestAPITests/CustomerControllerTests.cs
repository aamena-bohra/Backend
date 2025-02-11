using BlueHarvest.API.Controllers;
using BlueHarvestAPI;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BlueHarvestAPITests
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly CustomerController _controller;

        public CustomerControllerTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _controller = new CustomerController(_customerServiceMock.Object);
        }

        [Fact]
        public void GetCustomerInfo_ReturnsOkResult_WithCustomerInfo()
        {
            // Arrange
            var customerId = 1;
            var customer = new Customer
            {
                CustomerId = customerId,
                Name = "John",
                Surname = "Doe"
            };

            _customerServiceMock.Setup(service => service.CreateDummyCustomers());

            _customerServiceMock.Setup(service => service.GetCustomerInfo(customerId)).Returns(customer);

            // Act
            var result = _controller.GetCustomerInfo(customerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(customerId, returnValue.CustomerId);
            Assert.Equal("John", returnValue.Name);
            Assert.Equal("Doe", returnValue.Surname);
        }


        [Fact]
        public void GetCustomerInfo_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerId = 999;
            _customerServiceMock.Setup(service => service.GetCustomerInfo(customerId));

            // Act
            var result = _controller.GetCustomerInfo(customerId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
